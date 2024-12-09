using iTextSharp.text.pdf;
using iTextSharp.text;
using KidsFashion.Servicos.CadastrosBasicos;
using KidsFashion.Servicos.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KidsFashion.Servicos.Interfaces;

namespace KidsFashion.Servicos.Relatorios.Pedidos
{
    public class RelatorioPedidoGenerator : IRelatorioPDF
    {
        public async Task<MemoryStream> GerarRelatorioPDF()
        {
            var servico = new ServicoPedido();

            var pedidos = servico.ObterTodosCompletoRastreamento();

            //Criar documento
            Document doc = UtilitariosPDF.CreateDoc();

            // Memória para armazenar o PDF
            MemoryStream memoryStream = new MemoryStream();

            // Criação do escritor PDF
            var writer = PdfWriter.GetInstance(doc, memoryStream);

            // Abertura do documento para edição
            doc.Open();

            var titulo = UtilitariosPDF.CriarTitulo("Relatório de Pedidos");

            doc.Add(titulo);

            // Adicionar tabela ao documento
            PdfPTable tabela = CriarTabela();

            foreach (var item in pedidos.Result)
            {
                // Adiciona o Nome (alinhado à esquerda)
                PdfPCell nomeCell = new PdfPCell(new Phrase(item.Cliente.Nome));
                nomeCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                tabela.AddCell(nomeCell);

                // Adiciona o CPF (alinhado ao centro) com a data formatada
                PdfPCell cpfCell = new PdfPCell(new Phrase(item.DataPedido.ToString("dd/MM/yyyy")));
                cpfCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tabela.AddCell(cpfCell);

            }

            // Certifique-se de que a tabela preenche o restante da página corretamente
            tabela.KeepTogether = true;

            doc.Add(tabela);

            doc.Add(new Paragraph(" ")); // Isso adicionará um espaço em branco entre as linhas

            doc.Close();

            return memoryStream;
        }

        private PdfPTable CriarTabela()
        {
            // Criando uma tabela com três colunas
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100; // Preenche a largura da página
            table.SpacingBefore = 10f;   // Espaço antes da tabela
            table.SpacingAfter = 10f;    // Espaço depois da tabela

            // Definindo largura para cada coluna (Nome maior)
            float[] columnWidths = { 60f, 40f };
            table.SetWidths(columnWidths);

            // Fonte em negrito para o cabeçalho
            Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);

            // Cabeçalho: CLIENTE
            PdfPCell cell0 = new PdfPCell(new Phrase("CLIENTE", boldFont));
            cell0.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(cell0);

            // Cabeçalho: DATA PEDIDO
            PdfPCell cell1 = new PdfPCell(new Phrase("DATA PEDIDO", boldFont));
            cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(cell1);

            return table;
        }
    }
}
