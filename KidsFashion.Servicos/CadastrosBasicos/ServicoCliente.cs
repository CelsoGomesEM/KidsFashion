﻿using KidsFashion.Dominio;
using KidsFashion.Persistencia.Repositorios;
using KidsFashion.Persistencia;
using KidsFashion.Servicos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsFashion.Servicos.CadastrosBasicos
{
    public class ServicoCliente : ServicoAbstratoDeCadastro<Cliente, RepositorioCliente, PersistContext>
    {
        public async Task RemoverEnderecoPorClienteId(long clienteId)
        {
            using (var repositorio = new RepositorioCliente())
            {
                await repositorio.RemoverEnderecoPorClienteId(clienteId);
            }
        }
    }
}
