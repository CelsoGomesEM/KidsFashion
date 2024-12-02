using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsFashion.Servicos.Utilidades
{
    public class UtilitariosPDF
    {
        public static Document CreateDoc()
        {
            // Defina as margens desejadas em pontos
            float topMargin = 1f * 28.35f;   // Margem superior de 4,5 cm
            float leftMargin = 2f * 28.35f;    // Margem esquerda de 2 cm
            float rightMargin = 2f * 28.35f;   // Margem direita de 2 cm
            float bottomMargin = 2.5f * 28.35f; // Margem inferior de 2,5 cm

            // Criação do documento PDF com margens
            iTextSharp.text.Document doc = new Document(
                PageSize.A4,
                leftMargin,
                rightMargin,
                topMargin,
                bottomMargin
            );
            return doc;
        }

        public static Paragraph CrieParagrafo(Font fonte, string texto)
        {
            // Crie um parágrafo com texto em negrito e caixa alta
            Paragraph paragrafoListaAptos = new Paragraph();
            Chunk chunkParagrafoLista = new Chunk(texto, fonte);
            paragrafoListaAptos.Add(chunkParagrafoLista);
            return paragrafoListaAptos;
        }
        public static Paragraph CriarTitulo(string titulo)
        {
            // Crie uma fonte usando Helvetica, tamanho 15 e negrito
            Font font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20, BaseColor.BLACK);

            // Crie o título com alinhamento centralizado
            Paragraph paragraph = new Paragraph(titulo, font);
            paragraph.Alignment = Element.ALIGN_CENTER;
            paragraph.SpacingAfter = 20f; // Espaçamento após o título

            return paragraph;
        }

    }
}
