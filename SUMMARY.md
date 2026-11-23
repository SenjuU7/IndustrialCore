## Logika yang sudah kamu pakai

**1. Pemodelan data**
`SensorReading` mewakili satu baris CSV. Properti dengan `string.Empty` sebagai default dan `Nullable` aktif membuat compiler membantu mencegah null.

**2. Enum untuk pilihan tetap**
`MachineStatus` membatasi status hanya ke tiga nilai. Urutan nilainya (Normal < Warning < Critical) dimanfaatkan di `GetMachineStatus`, sehingga `Math.Max` otomatis memilih status terburuk. Ini trik yang bagus, tapi bergantung pada urutan enum, jadi jangan diubah sembarangan.

**3. Pembacaan data dan penanganan error**
`SensorRepository` hanya tahu cara membaca file. Ia mengecek jumlah kolom dan memakai `TryParse` dengan `CultureInfo.InvariantCulture`, sehingga titik desimal tidak tergantung pengaturan komputer. Pesan error menyebut nomor baris, yang sangat membantu saat debug.

**4. Validasi sebagai filter**
`ValidateReading` dipakai sebagai predicate di `Where`. Data yang tidak lolos disaring sebelum diolah, dan jumlahnya dihitung untuk laporan.

**5. Klasifikasi dan komposisi**
`Classify` adalah satu-satunya tempat yang membandingkan nilai dengan batas. Tiga method status memanggilnya, lalu `GetMachineStatus` menggabungkan hasilnya. Inilah contoh "jangan mengulang business rules" dari Task 03.

**6. Konstanta untuk aturan**
Threshold disimpan sebagai `const`. Kalau aturan berubah, cukup satu tempat yang diedit.

**7. LINQ untuk pengolahan data**
`Where`, `Select`, `Count`, `Average`, dan `ToList` dipakai untuk menyaring, mengubah, dan menghitung. Kamu juga sudah membuat fitur high-risk sendiri dengan pola yang sama.

**8. Pemisahan tanggung jawab**
`Models` menyimpan bentuk data, `Data` berurusan dengan file, `Services` berisi aturan, dan `Program` merakit semuanya. Dependency dimasukkan lewat constructor, bukan dibuat dengan `new` di dalam class.

**9. Path yang tidak bergantung pada working directory**
`AppContext.BaseDirectory` dikombinasikan dengan `.csproj` yang menyalin CSV ke folder output. Ini membuat program bisa dijalankan dari folder mana pun.

**10. Git dan dokumentasi**
Kamu sudah memecah pekerjaan ke commit dengan pesan yang jelas, membersihkan file yang tidak dipakai, dan menulis panduan baca kode.
