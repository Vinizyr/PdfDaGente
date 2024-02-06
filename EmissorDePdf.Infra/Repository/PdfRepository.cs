using Dapper;
using EmissorDePdf.Domain.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmissorDePdf.Infra.Repository
{
    public class PdfRepository : IPdfRepository
    {
        private readonly IConfiguration _configuration;

        public PdfRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SavePdf(FileContentResult pdfDocumento)
        {
            var sql = "INSERT INTO PdfDocumentos (DadosPdf) VALUES (@DadosPdf)";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            await connection.ExecuteAsync(sql, new
            {
                DadosPdf = pdfDocumento.FileContents
            });
        }
    }
}
