interface iBuku
{
    void DisplayInfo();
}

abstract class Buku : iBuku
{
    public string Judul { get; private set; }
    public string Penulis { get; private set; }
    public int Tahun_terbit { get; private set; }
    public bool Dipinjam { get; set; }

    public Buku(string judul, string penulis, int tahun_terbit)
    {
        Judul = judul;
        Penulis = penulis;
        Tahun_terbit = tahun_terbit;
        Dipinjam = false;
    }
    public abstract void DisplayInfo();
    public void Edit(string judul, string penulis, int tahun_terbit)
    {
        Judul = judul;
        Penulis = penulis;
        Tahun_terbit = tahun_terbit;
    }
}

class BukuFiksi : Buku
{
    public BukuFiksi(string judul, string penulis, int tahun_terbit) : base(judul, penulis, tahun_terbit) { }
    public override void DisplayInfo()
    {
        Console.WriteLine($"[Buku Fiksi] {Judul} Ditulis oleh {Penulis},{Tahun_terbit}");
    }
}

class BukuNonFiksi : Buku
{
    public BukuNonFiksi(string judul, string penulis, int tahun_terbit) : base(judul, penulis, tahun_terbit) { }
    public override void DisplayInfo()
    {
        Console.WriteLine($"[Buku Non - Fiksi] {Judul} Ditulis oleh {Penulis},{Tahun_terbit}");
    }
}

class Majalah : Buku
{
    public Majalah(string judul, string penulis, int tahun_terbit) : base(judul, penulis, tahun_terbit) { }
    public override void DisplayInfo()
    {
        Console.WriteLine($"[Majalah] {Judul} Ditulis oleh {Penulis},{Tahun_terbit}");
    }
}

class Pengguna
{
    public List<Buku> Buku_dipinjam { get; } = new List<Buku>();
    public bool Pinjam(Buku buku)
    {
        if (Buku_dipinjam.Count >= 3)
        {
            Console.WriteLine("Hanya dapat meminjam 3 buku!");
            return false;

        }
        if (buku.Dipinjam)
        {
            Console.WriteLine("Buku sedang dipinjam!");
            return false;
        }
        buku.Dipinjam = true;
        Buku_dipinjam.Add(buku);
        Console.WriteLine($"Berhasil meminjam buku: {buku.GetType().Name} - {buku.Judul}");
        return true;
    }
    public void Kembali(Buku buku)
    {
        if (Buku_dipinjam.Contains(buku))
        {
            buku.Dipinjam = false;
            Buku_dipinjam.Remove(buku);
            Console.WriteLine($"Buku {buku.Judul} berhasil dikembalikan!");
        }
        else
        {
            Console.WriteLine("Buku tidak sedang dipinjam");
        }
    }
    public void LiatDipinjam()
    {
        Console.WriteLine("Buku yang sedang dipinjam:");
        foreach (var book in Buku_dipinjam)
        {
            book.DisplayInfo();
        }
    }
}

class Perpus
{
    public List<Buku> Buku { get; } = new List<Buku>();

    public void TambahBuku(Buku buku)
    {
        Buku.Add(buku);
        Console.WriteLine("Buku berhasil ditambahkan!");
    }

    public void EditBuku(int index, string judul, string penulis, int tahun_terbit)
    {
        if (index >= 0 && index < Buku.Count)
        {
            Buku[index].Edit(judul, penulis, tahun_terbit);
            Console.WriteLine("Buku berhasil diubah!");
        }
        else
        {
            Console.WriteLine("Indeks tidak valid.");
        }
    }

    public void LiatSemua()
    {
        for (int i = 0; i < Buku.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            Buku[i].DisplayInfo();
        }
    }

    public Buku? GetBookByIndex(int index)
    {
        return (index >= 0 && index < Buku.Count) ? Buku[index] : null;
    }
}

class Program
{
    static void Main()
    {
        Perpus perpus = new Perpus();
        Pengguna pengguna = new Pengguna();

        while (true)
        {
            Console.WriteLine("\n--- Menu ---");
            Console.WriteLine("1. Tambah Buku");
            Console.WriteLine("2. Ubah Buku");
            Console.WriteLine("3. Lihat Semua Buku");
            Console.WriteLine("4. Pinjam Buku");
            Console.WriteLine("5. Kembalikan Buku");
            Console.WriteLine("6. Lihat Buku yang Dipinjam");
            Console.WriteLine("0. Keluar");
            Console.Write("Pilihan: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Jenis buku (1=Fiksi, 2=Non-Fiksi, 3=Majalah): ");
                    string type = Console.ReadLine();
                    Console.Write("Judul: ");
                    string judul = Console.ReadLine();
                    Console.Write("Penulis: ");
                    string penulis = Console.ReadLine();
                    Console.Write("Tahun: ");
                    int tahun_terbit = int.Parse(Console.ReadLine());

                    Buku buku = type switch
                    {
                        "1" => new BukuFiksi(judul, penulis, tahun_terbit),
                        "2" => new BukuNonFiksi(judul, penulis, tahun_terbit),
                        "3" => new Majalah(judul, penulis, tahun_terbit),
                        _ => null
                    };

                    if (buku != null)
                        perpus.TambahBuku(buku);
                    else
                        Console.WriteLine("Jenis tidak dikenali.");
                    break;

                case "2":
                    perpus.LiatSemua();
                    Console.Write("Pilih nomor buku yang ingin diubah: ");
                    int editIndex = int.Parse(Console.ReadLine()) - 1;
                    Console.Write("Judul baru: ");
                    string newJudul = Console.ReadLine();
                    Console.Write("Penulis baru: ");
                    string newPenulis = Console.ReadLine();
                    Console.Write("Tahun baru: ");
                    int newTahun_terbit = int.Parse(Console.ReadLine());
                    perpus.EditBuku(editIndex, newJudul, newPenulis, newTahun_terbit);
                    break;

                case "3":
                    perpus.LiatSemua();
                    break;

                case "4":
                    perpus.LiatSemua();
                    Console.Write("Pilih nomor buku yang ingin dipinjam: ");
                    int borrowIndex = int.Parse(Console.ReadLine()) - 1;
                    var borrowBook = perpus.GetBookByIndex(borrowIndex);
                    if (borrowBook != null)
                        pengguna.Pinjam(borrowBook);
                    else
                        Console.WriteLine("Buku tidak ditemukan.");
                    break;

                case "5":
                    pengguna.LiatDipinjam();
                    Console.Write("Pilih nomor buku untuk dikembalikan: ");
                    int returnIndex = int.Parse(Console.ReadLine()) - 1;
                    if (returnIndex >= 0 && returnIndex < pengguna.Buku_dipinjam.Count)
                        pengguna.Kembali(pengguna.Buku_dipinjam[returnIndex]);
                    else
                        Console.WriteLine("Indeks tidak valid.");
                    break;

                case "6":
                    pengguna.LiatDipinjam();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Pilihan tidak valid.");
                    break;
            }
        }
    }
}