using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsFashion.Servicos.Utilidades
{
    public class CustomHeaderFooterEvent : PdfPageEventHelper
    {
        private string imagePathHeader;

        public CustomHeaderFooterEvent(string imagePathHeader)
        {
            this.imagePathHeader = imagePathHeader;
        }

        public override void OnStartPage(PdfWriter writer, Document doc)
        {
            base.OnStartPage(writer, doc);

            // Crie uma tabela para o cabeçalho
            PdfPTable headerTable = new PdfPTable(1);
            headerTable.TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin;
            headerTable.DefaultCell.Border = PdfPCell.NO_BORDER;

            // Adicione a imagem do cabeçalho
            Image headerImage = Image.GetInstance(imagePathHeader);
            headerImage.ScaleToFit(200, 100); // Ajuste o tamanho da imagem conforme necessário

            PdfPCell headerCell = new PdfPCell(headerImage, true);
            headerCell.Border = PdfPCell.NO_BORDER;
            headerTable.AddCell(headerCell);

            // Defina a posição da imagem do cabeçalho
            float xPosition = doc.LeftMargin + ((doc.Right - doc.Left - headerImage.ScaledWidth) / 2);
            float yPosition = doc.PageSize.Height - doc.TopMargin - headerImage.ScaledHeight - 20; // 20 é o espaço adicionado após o cabeçalho
            headerImage.SetAbsolutePosition(xPosition, yPosition);

            // Escreva a tabela de cabeçalho na página atual
            headerTable.WriteSelectedRows(0, -1, doc.LeftMargin, doc.PageSize.Height - doc.TopMargin, writer.DirectContent);

            doc.Add(new Paragraph(" ")); // Isso adicionará um espaço em branco entre as linhas
            doc.Add(new Paragraph(" ")); // Isso adicionará um espaço em branco entre as linhas
            doc.Add(new Paragraph(" ")); // Isso adicionará um espaço em branco entre as linhas
            doc.Add(new Paragraph(" ")); // Isso adicionará um espaço em branco entre as linhas
            doc.Add(new Paragraph(" ")); // Isso adicionará um espaço em branco entre as linhas
            doc.Add(new Paragraph(" ")); // Isso adicionará um espaço em branco entre as linhas
            doc.Add(new Paragraph(" ")); // Isso adicionará um espaço em branco entre as linhas

        }
    }
}
