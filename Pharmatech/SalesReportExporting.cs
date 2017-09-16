using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data;

namespace Pharmatech
{
    public class SalesReportExporting
    {

        public static void ExportDataTableToPdf(DataTable dtblTable, String strPdfPath, string strHeader, string saleType, string startDate, string endDate, string medname, string patientid)
        {
            System.IO.FileStream fs = new FileStream(strPdfPath, FileMode.Create, FileAccess.Write, FileShare.None);
            Document document = new Document(PageSize.A4, 10, 10, 10, 10);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();

            //Report Header
            BaseFont bfntHead = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntHead = new Font(bfntHead, 18, 1, BaseColor.BLACK);
            Paragraph prgHeading = new Paragraph();
            prgHeading.Alignment = Element.ALIGN_CENTER;

            if (string.IsNullOrEmpty(saleType))
            {
                prgHeading.Add(new Chunk(strHeader.ToUpper() + " - ALL SALES", fntHead));
                document.Add(prgHeading);
            }

            else
            {
                prgHeading.Add(new Chunk(strHeader.ToUpper() + " - " + saleType, fntHead));
                document.Add(prgHeading);
            }

            //Author
            Paragraph prgAuthor = new Paragraph();
            BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntAuthor = new Font(btnAuthor, 10, 2, BaseColor.BLACK);
            prgAuthor.Alignment = Element.ALIGN_CENTER;
           
            prgAuthor.Add(new Chunk("Powered By PharmaTech" + "\n", fntAuthor));

            if (string.IsNullOrEmpty(endDate))
            {

                prgAuthor.Add(new Chunk("\n" + "From: " + startDate + " To: " + DateTime.Now.ToShortDateString() + "\n", fntAuthor));
            }

            else
            {
                prgAuthor.Add(new Chunk("\n" + "From: " + startDate + " To: " + endDate + "\n", fntAuthor));
            }

            if (!string.IsNullOrEmpty(patientid))
            {
                prgAuthor.Add(new Chunk("For Patient: " + patientid, fntAuthor));

            }

            //  prgAuthor.Add(new Chunk("\nDate Created: " + DateTime.Now.ToShortDateString(), fntAuthor));
            document.Add(prgAuthor);

            //Add a line seperation
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            document.Add(p);

            //Add line break
            document.Add(new Chunk("\n", fntHead));

            ////Add parameters chosen for sales report
            //Paragraph parameters = new Paragraph();
            //parameters.Alignment = Element.ALIGN_LEFT;
            
            //parameters.Add(new Chunk("Sale Type: " + saleType + "\n", fntAuthor));
            //parameters.Add(new Chunk("By medicine name: " + medname + "\n", fntAuthor));
            //parameters.Add(new Chunk("By Patient: " + patientid, fntAuthor));
            //document.Add(parameters);

            // Add another line break
            document.Add(new Chunk("\n", fntHead));

            //Write the table
            PdfPTable table = new PdfPTable(dtblTable.Columns.Count);
            table.SetWidths(new int[] { 1, 1, 1, 2, 1, 1});
            //Table header
            BaseFont btnColumnHeader = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntColumnHeader = new Font(btnColumnHeader, 8, 0, BaseColor.BLACK);

            for (int i = 0; i < dtblTable.Columns.Count; i++)
            {
                PdfPCell cell = new PdfPCell();
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.AddElement(new Chunk(dtblTable.Columns[i].ColumnName, fntColumnHeader));
                table.AddCell(cell);
            }
            //table Data
            for (int i = 0; i < dtblTable.Rows.Count; i++)
            {
                for (int j = 0; j < dtblTable.Columns.Count; j++)
                {
                    table.AddCell(new Phrase(dtblTable.Rows[i][j].ToString(), fntColumnHeader));
                }
            }
            
            document.Add(table);
            document.Add(new Chunk("\n", fntHead));
            Paragraph linebreak = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            document.Add(linebreak);
            document.Close();
            writer.Close();
            fs.Close();
        }






        public static void ExportToInvoice(DataTable dtblTable, String strPdfPath, string strHeader, string patientName, string itemNames, int salePrice, double vatAmount, double subTotal)
        {
            System.IO.FileStream fs = new FileStream(strPdfPath, FileMode.Create, FileAccess.Write, FileShare.None);
            Document document = new Document(PageSize.A4, 10, 10, 10, 10);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();

            //Report Header
            BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntHead = new Font(bfntHead, 16, 1, BaseColor.BLACK);
            Paragraph prgHeading = new Paragraph();
            prgHeading.Alignment = Element.ALIGN_CENTER;
            prgHeading.Add(new Chunk(strHeader.ToUpper(), fntHead));
            document.Add(prgHeading);

            //Author
            Paragraph prgAuthor = new Paragraph();
            BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntAuthor = new Font(btnAuthor, 10, 2, BaseColor.BLACK);
            prgAuthor.Alignment = Element.ALIGN_CENTER;
            prgAuthor.Add(new Chunk("Powered By PharmaTech", fntAuthor));
            prgAuthor.Add(new Chunk("\nDate Issued: " + DateTime.Now.ToShortDateString(), fntAuthor));
            document.Add(prgAuthor);

            //Add a line seperation
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            document.Add(p);

            //Add line break
            document.Add(new Chunk("\n", fntHead));

            //Add parameters chosen for sales report
            Paragraph parameters = new Paragraph();
            parameters.Alignment = Element.ALIGN_CENTER;
            parameters.Add(new Chunk("Patient Name: " + patientName, fntAuthor));
            document.Add(parameters);

            // Add another line break
            document.Add(new Chunk("\n", fntHead));

            //Write the table
            PdfPTable table = new PdfPTable(dtblTable.Columns.Count);
            table.SetWidths(new int[] {1, 1, 1, 1, 1, 1});
            //Table header
            BaseFont btnColumnHeader = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntColumnHeader = new Font(btnColumnHeader, 8, 0, BaseColor.BLACK);

            for (int i = 0; i < dtblTable.Columns.Count; i++)
            {
                PdfPCell cell = new PdfPCell();

                cell.HorizontalAlignment = 1;
                //cell.VerticalAlignment = Element.ALIGN_CENTER;
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.AddElement(new Chunk(dtblTable.Columns[i].ColumnName, fntColumnHeader));
                table.AddCell(cell);
            }
            //table Data
            for (int i = 0; i < dtblTable.Rows.Count; i++)
            {
                for (int j = 0; j < dtblTable.Columns.Count; j++)
                {
                    table.AddCell(new Phrase(dtblTable.Rows[i][j].ToString(), fntColumnHeader));
                }
            }

            document.Add(table);

            //Add another space and a linebreak
            document.Add(new Chunk("\n", fntHead));
            Paragraph linebreak = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            document.Add(linebreak);

            Paragraph total = new Paragraph();
            total.Alignment = Element.ALIGN_RIGHT;
            total.Add(new Chunk("Sub-Total: R" +  subTotal + "\n", fntAuthor));
            total.Add(new Chunk("VAT Amount: R " + vatAmount + "\n", fntAuthor));
            total.Add(new Chunk("TOTAL: R " + salePrice, fntAuthor));
            document.Add(total);
            document.Close();
            writer.Close();
            fs.Close();
        }


        //public static void GeneratePDF(DataGrid grid)
        //{
        //    System.IO.FileStream fs = new System.IO.FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\TestReport" + DateTime.Now.ToString("MMddyyyy"), System.IO.FileMode.Create);
        //    Document document = new Document(PageSize.A4, 70, 30, 50, 30);
        //    PdfWriter writer = PdfWriter.GetInstance(document, fs);

        //    document.AddAuthor("Dean Wiseman");
        //    document.AddCreator("Test PDF Report");
        //    document.AddKeywords("Sales Report, powered by PharmaTech");
        //    document.AddSubject("Document subject - Describing the steps creating a PDF document");
        //    document.AddTitle("The document title - PDF creation using iTextSharp");

        //    PdfPTable table = new PdfPTable(grid.Columns.Count);
        //    document.Open();
        //    for (int j = 0; j < grid.Columns.Count; j++)
        //    {
        //        table.AddCell(new Phrase(grid.Columns[j].Header.ToString()));
        //    }
        //    table.HeaderRows = 1;
        //    System.Collections.IEnumerable itemsSource = grid.ItemsSource as System.Collections.IEnumerable;

        //    if (itemsSource != null)
        //    {
        //        foreach (var item in itemsSource)
        //        {
        //            DataGridRow row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
        //            if (row != null)
        //            {
        //                DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);
        //                for (int i = 0; i < grid.Columns.Count; ++i)
        //                {
        //                    DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
        //                    TextBlock txt = cell.Content as TextBlock;
        //                    if (txt != null)
        //                    {
        //                        table.AddCell(new Phrase(txt.Text));
        //                    }
        //                }
        //            }
        //        }





        //        PdfContentByte cb = writer.DirectContent;
        //    BaseFont stockFont = BaseFont.CreateFont("c:\\windows\\fonts\\calibri.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        //    cb.BeginText();
        //    cb.SetFontAndSize(stockFont, 20);
        //    cb.SetTextMatrix(230, 780);
        //    cb.ShowText("Pharmacy Sales Report");
        //    cb.SetFontAndSize(stockFont, 8);
        //    cb.SetTextMatrix(100, 100);
        //    cb.ShowText("Powered by PharmaTech");
        //    cb.EndText();


        //    document.Add(new iTextSharp.text.Paragraph(""));
        //    document.Add(new iTextSharp.text.Paragraph("Today's Date: " + DateTime.Now.ToString("MM/dd/yyyy")));
        //    document.Add(new iTextSharp.text.Paragraph("Selected Parameters: "));
        //    document.Add(new iTextSharp.text.Paragraph("Type of Sale: "));
        //    document.Add(new iTextSharp.text.Paragraph("Date From: "));
        //    document.Add(new iTextSharp.text.Paragraph("Date To: "));






        //    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance("images/Pharmatech 2 - Transparrent.png");
        //    img.SetAbsolutePosition(100, 105);
        //    img.ScaleAbsolute(216, 70);
        //    cb.AddImage(img);



        //    document.Close();           
        //    writer.Close();
        //    fs.Close();


        //}
    }
}
