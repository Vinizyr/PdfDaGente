using EmissorDePdf.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmissorDePdf.Domain.IRepository
{
    public interface IPdfRepository
    {
        Task SavePdf(FileContentResult pdfDocumento);
    }
}
