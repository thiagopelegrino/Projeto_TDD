using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.WsFederation;
using Newtonsoft.Json;
using Projeto.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Projeto.Tests
{
    public class ClienteTest
    {
        //atributos..
        private readonly AppContext appContext;
        private readonly string endpoint;

        //construtor -> ctor + 2x[tab]
        public ClienteTest()
        {
            appContext = new AppContext();
            endpoint = "/api/Cliente";
        }

        [Fact] //método para execução de teste do XUnit
        //async -> método executado como uma Thread (assincrono)
        public async Task Cliente_Post_ReturnsOk()
        {
            //cadastrando um cliente na API
            var modelCadastro = new ClienteCadastroModel()
            {
                Nome = "Sergio Mendes",
                Email = "sergio.coti@gmail.com"
            };

            var requestCadastro = new StringContent(JsonConvert.SerializeObject(modelCadastro),
                            Encoding.UTF8, "application/json");
            var responseCadastro = await appContext.Client.PostAsync(endpoint, requestCadastro);

            var resultCadastro = string.Empty;
            using (HttpContent content = responseCadastro.Content)
            {
                Task<string> r = content.ReadAsStringAsync();
                resultCadastro += r.Result;
            }

            var resposta = JsonConvert.DeserializeObject<ResultModel>(resultCadastro);

            //verificação de teste
            responseCadastro.StatusCode.Should().Be(HttpStatusCode.OK);
            resposta.Mensagem.Should().Be("Cliente cadastrado com sucesso.");
        }

        [Fact] //método para execução de teste do XUnit
        //async -> método executado como uma Thread (assincrono)
        public async Task Cliente_Post_ReturnsBadRequest()
        {
            //preencher os campos da model
            var model = new ClienteCadastroModel()
            {
                Nome = string.Empty, //vazio
                Email = string.Empty //vazio
            };

            //montando os dados em JSON que serão enviados para a API
            var request = new StringContent(JsonConvert.SerializeObject(model),
                            Encoding.UTF8, "application/json");

            //executando o serviço da API..
            var response = await appContext.Client.PostAsync(endpoint, request);

            //critério de teste (Serviço da API retornar HTTP BADREQUEST (400))
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact] //método para execução de teste do XUnit
        //async -> método executado como uma Thread (assincrono)
        public async Task Cliente_Put_ReturnsOk()
        {
            //--------------cadastrando um cliente na API
            var modelCadastro = new ClienteCadastroModel()
            {
                Nome = "Sergio Mendes",
                Email = "sergio.coti@gmail.com"
            };

            var requestCadastro = new StringContent(JsonConvert.SerializeObject(modelCadastro),
                            Encoding.UTF8, "application/json");
            var responseCadastro = await appContext.Client.PostAsync(endpoint, requestCadastro);

            var resultCadastro = string.Empty;
            using (HttpContent content = responseCadastro.Content)
            {
                Task<string> r = content.ReadAsStringAsync();
                resultCadastro += r.Result;
            }

            var respostaCadastro = JsonConvert.DeserializeObject<ResultModel>(resultCadastro);

            //verificação de teste
            responseCadastro.StatusCode.Should().Be(HttpStatusCode.OK);
            respostaCadastro.Mensagem.Should().Be("Cliente cadastrado com sucesso.");


            //--------------atualizando o cliente cadastrado na API
            var modelEdicao = new ClienteEdicaoModel()
            {
                IdCliente = respostaCadastro.Cliente.IdCliente,
                Nome = "Sergio da Silva Mendes",
                Email = "sergio.coti@yahoo.com"
            };

            var requestEdicao = new StringContent(JsonConvert.SerializeObject(modelEdicao),
                            Encoding.UTF8, "application/json");
            var responseEdicao = await appContext.Client.PutAsync(endpoint, requestEdicao);

            var resultEdicao = string.Empty;
            using (HttpContent content = responseEdicao.Content)
            {
                Task<string> r = content.ReadAsStringAsync();
                resultEdicao += r.Result;
            }

            var respostaEdicao = JsonConvert.DeserializeObject<ResultModel>(resultEdicao);

            //verificação de teste
            responseEdicao.StatusCode.Should().Be(HttpStatusCode.OK);
            respostaEdicao.Mensagem.Should().Be("Cliente atualizado com sucesso.");
        }

        [Fact] //método para execução de teste do XUnit
        //async -> método executado como uma Thread (assincrono)
        public async Task Cliente_Put_ReturnsBadRequest()
        {
            //preencher os campos da model
            var model = new ClienteEdicaoModel()
            {
                IdCliente = 0,
                Nome = string.Empty, //vazio
                Email = string.Empty //vazio
            };

            //montando os dados em JSON que serão enviados para a API
            var request = new StringContent(JsonConvert.SerializeObject(model),
                            Encoding.UTF8, "application/json");

            //executando o serviço da API..
            var response = await appContext.Client.PutAsync(endpoint, request);

            //critério de teste (Serviço da API retornar HTTP BADREQUEST (400))
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact] //método para execução de teste do XUnit
        //async -> método executado como uma Thread (assincrono)
        public async Task Cliente_Delete_ReturnsOk()
        {
            //--------------cadastrando um cliente na API
            var modelCadastro = new ClienteCadastroModel()
            {
                Nome = "Sergio Mendes",
                Email = "sergio.coti@gmail.com"
            };

            var requestCadastro = new StringContent(JsonConvert.SerializeObject(modelCadastro),
                            Encoding.UTF8, "application/json");
            var responseCadastro = await appContext.Client.PostAsync(endpoint, requestCadastro);

            var resultCadastro = string.Empty;
            using (HttpContent content = responseCadastro.Content)
            {
                Task<string> r = content.ReadAsStringAsync();
                resultCadastro += r.Result;
            }

            var respostaCadastro = JsonConvert.DeserializeObject<ResultModel>(resultCadastro);

            //verificação de teste
            responseCadastro.StatusCode.Should().Be(HttpStatusCode.OK);
            respostaCadastro.Mensagem.Should().Be("Cliente cadastrado com sucesso.");


            //--------------excluindo o cliente cadastrado na API  
            var responseExclusao = await appContext.Client.DeleteAsync
                (endpoint + "/" + respostaCadastro.Cliente.IdCliente);

            var resultExclusao = string.Empty;
            using (HttpContent content = responseExclusao.Content)
            {
                Task<string> r = content.ReadAsStringAsync();
                resultExclusao += r.Result;
            }

            var respostaExclusao = JsonConvert.DeserializeObject<ResultModel>(resultExclusao);

            //verificação de teste
            responseExclusao.StatusCode.Should().Be(HttpStatusCode.OK);
            respostaExclusao.Mensagem.Should().Be("Cliente excluido com sucesso.");
        }

        [Fact] //método para execução de teste do XUnit
        //async -> método executado como uma Thread (assincrono)
        public async Task Cliente_GetAll_ReturnsOk()
        {
            //executando o serviço da API..
            var response = await appContext.Client.GetAsync(endpoint);

            //critério de teste (Serviço da API retornar HTTP OK (200))
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact] //método para execução de teste do XUnit
        //async -> método executado como uma Thread (assincrono)
        public async Task Cliente_GetById_ReturnsOk()
        {
            //executando o serviço da API..
            var response = await appContext.Client.GetAsync(endpoint + "/" + 1);

            //critério de teste (Serviço da API retornar HTTP OK (200))
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }

    //Modelos de dados para obter o retorno da API..
    public class ResultModel
    {
        public string Mensagem { get; set; }
        public Cliente Cliente { get; set; }
    }

    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
