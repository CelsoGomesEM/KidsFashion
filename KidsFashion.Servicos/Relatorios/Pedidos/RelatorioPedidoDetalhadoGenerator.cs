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
    public class RelatorioPedidoDetalhadoGenerator : IRelatorioPDF
    {

        public int PedidoId { get; set; }

        public RelatorioPedidoDetalhadoGenerator(int id)
        {
            this.PedidoId = id;
        }
        public async Task<MemoryStream> GerarRelatorioPDF()
        {
            var servico = new ServicoPedido();

            var pedidos = servico.ObterTodosCompletoRastreamento();

            var pedido = pedidos.Result.Where(c => c.Id == PedidoId).FirstOrDefault();

            // Criar documento
            Document doc = UtilitariosPDF.CreateDoc();

            // Memória para armazenar o PDF
            MemoryStream memoryStream = new MemoryStream();

            // Criação do escritor PDF
            var writer = PdfWriter.GetInstance(doc, memoryStream);

            // Abertura do documento para edição
            doc.Open();

            var titulo = UtilitariosPDF.CriarTitulo("Relatório de Pedidos");
            doc.Add(titulo);

            // Fonte para cabeçalhos e texto
            Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            Font normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);

            // Criar tabela de Cliente e Data do Pedido
            PdfPTable tabelaPedido = new PdfPTable(2);
            tabelaPedido.WidthPercentage = 100;
            tabelaPedido.SetWidths(new float[] { 50f, 50f });

            // Cabeçalhos da tabela
            tabelaPedido.AddCell(new PdfPCell(new Phrase("CLIENTE", boldFont)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER });
            tabelaPedido.AddCell(new PdfPCell(new Phrase("DATA PEDIDO", boldFont)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER });

            // Dados do Cliente e Data do Pedido
            tabelaPedido.AddCell(new PdfPCell(new Phrase(pedido.Cliente.Nome, normalFont)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT });
            tabelaPedido.AddCell(new PdfPCell(new Phrase(pedido.DataPedido.ToString("dd/MM/yyyy"), normalFont)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER });

            doc.Add(tabelaPedido);

            // Criar tabela de Produto, Categoria e Quantidade
            PdfPTable tabelaItens = new PdfPTable(3);
            tabelaItens.WidthPercentage = 100;
            tabelaItens.SetWidths(new float[] { 50f, 30f, 20f });

            // Cabeçalhos da tabela de itens
            tabelaItens.AddCell(new PdfPCell(new Phrase("PRODUTO", boldFont)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER });
            tabelaItens.AddCell(new PdfPCell(new Phrase("CATEGORIA", boldFont)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER });
            tabelaItens.AddCell(new PdfPCell(new Phrase("QUANTIDADE", boldFont)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER });

            // Adicionar os itens do pedido
            foreach (var item in pedido.PedidoProdutos)
            {
                tabelaItens.AddCell(new PdfPCell(new Phrase(item.Produto.Nome, normalFont)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT });
                tabelaItens.AddCell(new PdfPCell(new Phrase(item.Produto.Categoria.Descricao, normalFont)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER });
                tabelaItens.AddCell(new PdfPCell(new Phrase(item.Quantidade.ToString(), normalFont)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT });
            }

            doc.Add(tabelaItens);

            doc.Close();

            return memoryStream;
        }

    }
}
