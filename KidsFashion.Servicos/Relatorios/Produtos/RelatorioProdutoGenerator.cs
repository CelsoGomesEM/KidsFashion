using iTextSharp.text.pdf;
using iTextSharp.text;
using KidsFashion.Servicos.CadastrosBasicos;
using KidsFashion.Servicos.Interfaces;
using KidsFashion.Servicos.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsFashion.Servicos.Relatorios.Produtos
{
    public class RelatorioProdutoGenerator : IRelatorioPDF
    {
        public async Task<MemoryStream> GerarRelatorioPDF()
        {
            var servico = new ServicoProduto();

            var produtos = servico.ObterTodos();

            //Criar documento
            Document doc = UtilitariosPDF.CreateDoc();

            // Memória para armazenar o PDF
            MemoryStream memoryStream = new MemoryStream();

            // Criação do escritor PDF
            var writer = PdfWriter.GetInstance(doc, memoryStream);

            // Abertura do documento para edição
            doc.Open();

            var titulo = UtilitariosPDF.CriarTitulo("Relatório de Clientes");

            doc.Add(titulo);

            // Adicionar tabela ao documento
            PdfPTable tabela = CriarTabela();

            foreach (var item in produtos.Result)
            {
                // Adiciona o Nome (alinhado à esquerda)
                PdfPCell nomeCell = new PdfPCell(new Phrase(item.Nome));
                nomeCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                tabela.AddCell(nomeCell);

                // Adiciona o CPF (alinhado ao centro)
                PdfPCell cpfCell = new PdfPCell(new Phrase(item.Descricao));
                cpfCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                tabela.AddCell(cpfCell);
            }

            // Certifique-se de que a tabela preenche o restante da página corretamente
            tabela.KeepTogether = true;

            doc.Add(tabela);

            doc.Add(new Paragraph(" ")); // Isso adicionará um espaço em branco entre as linhas

            doc.Close();

            return memoryStream;
        }
        private static PdfPTable CriarTabela()
        {
            // Criando uma tabela com três colunas
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100; // Preenche a largura da página
            table.SpacingBefore = 10f;   // Espaço antes da tabela
            table.SpacingAfter = 10f;    // Espaço depois da tabela

            // Definindo largura para cada coluna (Nome maior)
            float[] columnWidths = { 40f, 60f };
            table.SetWidths(columnWidths);

            // Fonte em negrito para o cabeçalho
            Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);

            // Cabeçalho: Nome
            PdfPCell cell0 = new PdfPCell(new Phrase("NOME", boldFont));
            cell0.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(cell0);

            // Cabeçalho: CPF
            PdfPCell cell1 = new PdfPCell(new Phrase("DESCRIÇÃO", boldFont));
            cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(cell1);

            return table;
        }
    }
}
