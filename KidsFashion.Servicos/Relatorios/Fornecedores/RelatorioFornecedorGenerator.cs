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

namespace KidsFashion.Servicos.Relatorios.Fornecedores
{
    public class RelatorioFornecedorGenerator : IRelatorioPDF
    {
        public async Task<MemoryStream> GerarRelatorioPDF()
        {
            var servico = new ServicoFornecedor();

            var fornecedores = servico.ObterTodos();

            //Criar documento
            Document doc = UtilitariosPDF.CreateDoc();

            // Memória para armazenar o PDF
            MemoryStream memoryStream = new MemoryStream();

            // Criação do escritor PDF
            var writer = PdfWriter.GetInstance(doc, memoryStream);

            // Abertura do documento para edição
            doc.Open();

            var titulo = UtilitariosPDF.CriarTitulo("Relatório de Fornecedores");

            doc.Add(titulo);

            // Adicionar tabela ao documento
            PdfPTable tabela = CriarTabela();

            foreach (var item in fornecedores.Result)
            {
                // Adiciona o Nome (alinhado à esquerda)
                PdfPCell nomeCell = new PdfPCell(new Phrase(item.Nome));
                nomeCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                tabela.AddCell(nomeCell);

                // Adiciona o CPF (alinhado ao centro)
                PdfPCell cpfCell = new PdfPCell(new Phrase(item.CPF_CNPJ));
                cpfCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tabela.AddCell(cpfCell);

                // Adiciona o Contato (alinhado ao centro)
                PdfPCell contatoCell = new PdfPCell(new Phrase(item.Contato));
                contatoCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tabela.AddCell(contatoCell);
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
            PdfPTable table = new PdfPTable(3);
            table.WidthPercentage = 100; // Preenche a largura da página
            table.SpacingBefore = 10f;   // Espaço antes da tabela
            table.SpacingAfter = 10f;    // Espaço depois da tabela

            // Definindo largura para cada coluna (Nome maior)
            float[] columnWidths = { 40f, 30f, 30f };
            table.SetWidths(columnWidths);

            // Fonte em negrito para o cabeçalho
            Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);

            // Cabeçalho: Nome
            PdfPCell cell0 = new PdfPCell(new Phrase("NOME", boldFont));
            cell0.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(cell0);

            // Cabeçalho: CPF
            PdfPCell cell1 = new PdfPCell(new Phrase("CPF", boldFont));
            cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(cell1);

            // Cabeçalho: Contato
            PdfPCell cell2 = new PdfPCell(new Phrase("CONTATO", boldFont));
            cell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(cell2);

            return table;
        }
    }
}
