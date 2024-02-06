using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmissorDePdf.Domain.Models
{
    public class NotaFiscal
    {
        public int NotaFiscalId { get; set; }
        public DateTime DataEmissao { get; set; } = DateTime.Now;
        public float ValorTotal { get; set; }
        public string OrgaoEmissor { get; set; }


        public int ClienteId { get; set; }
    }
}
