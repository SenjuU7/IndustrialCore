## **Challenge berikutnya: Student Management System**

Gue ingin lo mengembangkan program yang sekarang, bukan membuangnya.

**Challenge 02 — Class Design**

Intermediate

Target struktur:

```
StudentApp/
├── Models/
│   └── Student.cs
├── Data/
│   └── StudentRepository.cs
├── Services/
│   └── StudentService.cs
└── Program.cs
```

**Tugas lo:**

1. Pindahkan perhitungan `Avg()` ke `StudentService`.
2. Pindahkan `Grade()` ke `StudentService`.
3. Buat `Student` sebagai model yang menyimpan nama dan nilai.
4. Pastikan `Program.cs` hanya bertugas menjalankan aplikasi.
5. Tambahkan validasi agar array nilai tidak kosong.

**Kriteria keberhasilan:**

- Tidak ada perhitungan nilai di `Program.cs`.
- Repository hanya mengelola data.
- Service mengelola logika bisnis.
- Model merepresentasikan data student.

