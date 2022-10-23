using System;
using System.Collections.Generic;


namespace RAS.Bootcamp.Net
{
    public class BarangRequest
    {
        public string Kode { get; set; } = null!;
        public string Nama { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Harga { get; set; }
        public int Stok { get; set; }
        public int IdPenjual { get; set; }
        public IFormFile? FileImage { get; set; } //mengirimkan data dari view, dari controller didapetin file fisiknya di ww root. nama filenya dan urlnya kita simpan ke database
    }
}
