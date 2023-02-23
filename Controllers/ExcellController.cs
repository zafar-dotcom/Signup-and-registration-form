using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MySqlConnector;
using System.Data.OleDb;
using System.Data;
using Microsoft.AspNetCore.Hosting;
using Segment.Model;
using System.IO;
using System.Configuration;
using Complete.Models;
using ClosedXML.Excel;
using System.Collections;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.text.html.simpleparser;
using Complete.Repository;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Drawing;
using System.Net;


namespace Complete.Controllers
{
    public class ExcellController : Controller
    {
        private readonly IExport_To_Excell exportinterface;
        private readonly IExport_pdf exportpdf;
        public ExcellController(IExport_To_Excell iexport,IExport_pdf iexportpdf) 
        { 
            this.exportinterface = iexport;
            this.exportpdf = iexportpdf;
        }

        public ActionResult index()
        {
            return View();
        }
        public IActionResult ImportExcellSheet()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ImportExcellSheet(IFormFile formFile)
        {
            var filename = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.ToString().Trim('"');
            var mainpath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload");
            if (!Directory.Exists(mainpath))
            {
                Directory.CreateDirectory(mainpath);
            }
            //get file path
            var filepath = System.IO.Path.Combine(mainpath, formFile.FileName);
            using (Stream stream = new FileStream(filepath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);

            }
            string constring = string.Empty;
            string fileextension = System.IO.Path.GetExtension(filename);
            switch (fileextension)
            {
                case ".xls":
                    constring = "provider = microsoft.jet.oledb.4.0;" +
                    " data source = " + filepath + ";" +
                    " extended properties = 'excel 8.0;hdr=yes;imex=1'";
                    break;
                case ".xlsx":
                    constring = "provider=microsoft.ace.oledb.12.0;" +
                    "data source=" + filepath + ";extended properties='excel 8.0;hdr=yes;imex=1'";
                    break;
            }
            DataTable tbl = new DataTable();
            constring = string.Format(constring, filepath);
            using (OleDbConnection conexcel = new OleDbConnection(constring))
            {
                using (OleDbCommand cmdecel = new OleDbCommand())
                {
                    conexcel.Open();
                    using (OleDbDataAdapter oda = new OleDbDataAdapter())
                    {

                        cmdecel.Connection = conexcel;

                        DataTable dtexcel;
                        dtexcel = conexcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        string sheetname = dtexcel.Rows[0]["table_name"].ToString();
                        conexcel.Close();

                        conexcel.Open();
                        cmdecel.CommandText = "select * from [" + sheetname + "]";
                        oda.SelectCommand = cmdecel;
                        oda.Fill(tbl);
                        conexcel.Close();


                    }
                }

            }
            try
            {
                
                string contring = "server=localhost;port=3306;uid=root;pwd=sobiazafar@2023;database=mvc_crud;allowloadlocalinfile=true";
                using (MySqlConnection conn = new MySqlConnection(contring))
                {
                    conn.Open();
                    MySqlBulkCopy mysqlbulkcopy = new MySqlBulkCopy(conn);

                    mysqlbulkcopy.DestinationTableName = "excel_table";
                    List<MySqlBulkCopyColumnMapping> mapings = new List<MySqlBulkCopyColumnMapping>
                {
                    new MySqlBulkCopyColumnMapping{SourceOrdinal=0,DestinationColumn="sn" },
                     new MySqlBulkCopyColumnMapping{SourceOrdinal =1,DestinationColumn="fname" },
                      new MySqlBulkCopyColumnMapping{SourceOrdinal=2,DestinationColumn="lname" },
                       new MySqlBulkCopyColumnMapping{SourceOrdinal=3,DestinationColumn="department" }

                };
                    foreach (MySqlBulkCopyColumnMapping mapin in mapings)
                    {
                        mysqlbulkcopy.ColumnMappings.Add(mapin);
                    }
                    //mysqlbulkcopycolumnmapping columnmaping = new mysqlbulkcopycolumnmapping {
                    //    sourceordinal=0,
                    //    destinationcolumn="sn"

                    //};
                    //mysqlbulkcopy.columnmappings.add(columnmaping);
                    //mysqlbulkcopycolumnmapping cl = new mysqlbulkcopycolumnmapping
                    //{
                    //    sourceordinal = 1,
                    //    destinationcolumn = "fname"

                    //};
                    //mysqlbulkcopy.columnmappings.add(cl);
                    //mysqlbulkcopycolumnmapping cl1= new mysqlbulkcopycolumnmapping
                    //{
                    //    sourceordinal = 2,
                    //    destinationcolumn = "lname"

                    //};
                    //mysqlbulkcopy.columnmappings.add(cl1) ;
                    //mysqlbulkcopycolumnmapping cl2= new mysqlbulkcopycolumnmapping
                    //{
                    //    sourceordinal = 3,
                    //    destinationcolumn = "department"

                    //};
                    //mysqlbulkcopy.columnmappings.add(cl2);
                    mysqlbulkcopy.WriteToServer(tbl);
                    conn.Close();
                    ViewBag.Message = "file imported ";
                    return View("importexcellsheet");
                }

            }
            catch (Exception)
            {

                throw;
            }
            //  ViewBag.Message = "file imported ";

        }
        
        /*public ActionResult Export()
        {
            DataTable tbl = exportinterface.Exportfromdatabase().Tables[0];
            List<ExportToExcellModel> modellist = new List<ExportToExcellModel>();
            ExportToExcellModel mdl = new ExportToExcellModel();
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
               
                modellist.Add(new ExportToExcellModel()
                {
                    FName = tbl.Rows[i]["firstname"].ToString(),
                    LName = tbl.Rows[i]["lastname"].ToString(),
                    Job = tbl.Rows[i]["job"].ToString(),
                    Amount = (float)tbl.Rows[i]["amount"],
                    Date_time = (DateTime)tbl.Rows[i]["date_time"] 
                }
            );
            }

            return View(modellist);
        }*/
        [HttpPost]
     public ActionResult Export_To_excell()
        {
            var arylist = exportinterface.Exportcustomer();
            using (XLWorkbook xl=new XLWorkbook())
            {
                xl.Worksheets.Add(arylist);
                using(MemoryStream memorystrean=new MemoryStream())
                {
                    xl.SaveAs(memorystrean);
                    return File(memorystrean.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Customer.xlsx");
                }
            }
        }



        // Export to pdf 


        public ActionResult Exportpdf()
        {
            List<ExportPdfEmployeemodel> lst=exportpdf.Get_Employee_Export_Pdfs();
            return View(lst);
        }
        [HttpPost]

        [System.Web.Mvc.ValidateInput(false)]
        public FileResult ExportToPdf(string GridHtml)
        {
           
            using (MemoryStream memorystream=new MemoryStream())
            {   
                StringReader sr=new StringReader(GridHtml);
                iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter pdfwriter = PdfWriter.GetInstance(pdfDoc, memorystream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(pdfwriter, pdfDoc, sr);
                pdfDoc.Close();
                return File(memorystream.ToArray(), "application/pdf", "Grid.pdf");
            }
        }
    }
}
