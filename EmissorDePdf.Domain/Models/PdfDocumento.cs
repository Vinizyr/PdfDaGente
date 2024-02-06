using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmissorDePdf.Domain.Models
{
    public class PdfDocumento
    {
        public int Id { get; set; }
        public byte[] DadosPdf { get; set; }
    }
}
