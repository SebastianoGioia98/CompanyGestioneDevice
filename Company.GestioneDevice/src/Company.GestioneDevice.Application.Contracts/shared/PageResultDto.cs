using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.GestioneDevice.shared;

public class PagedResultDto<T>
{
    public long TotalCount { get; set; } // Numero totale di elementi
    public int TotalPages { get; set; } // Numero totale di pagine
    public List<T> Items { get; set; } // Lista degli elementi della pagina corrente
}
