# Panduan Membaca Kode IndustrialCore (Versi Refactor)

Dokumen ini menjelaskan cara membaca kode hasil perbaikan, urutan yang disarankan, dan apa yang berubah dari versi aslimu.

---

## 1. Gambaran besar

Program sekarang dibagi menjadi empat lapisan. Data mengalir dari atas ke bawah:

```
Program.cs                      (titik masuk: merakit objek dan menjalankan)
   │
   ▼
Services/MonitoringService.cs   (alur: ambil → validasi → klasifikasi → tampilkan)
   │            │
   ▼            ▼
Data/           Services/
SensorRepository.cs  SensorService.cs   (aturan bisnis: threshold & status)
   │
   ▼
Models/         (bentuk data: SensorReading, MachineStatus, SensorSummary)
```

Aturan praktisnya: **Data** hanya tahu cara membaca file, **Services/SensorService** hanya tahu aturan bisnis, dan **MonitoringService** hanya menghubungkan keduanya.

---

## 2. Urutan membaca yang disarankan

Baca dari yang paling sederhana, lalu yang paling kompleks:

1. **`Models/SensorReading.cs`**: bentuk satu baris data. Hanya lima property.
2. **`Models/MachineStatus.cs`**: tiga kemungkinan status (Normal, Warning, Critical). Nilainya berurutan supaya bisa dibandingkan.
3. **`Models/SensorSummary.cs`**: hasil ringkasan untuk Task 04 (jumlah sensor, rata-rata, hitungan per status).
4. **`Data/SensorRepository.cs`**: membaca CSV dan mengubah tiap baris menjadi `SensorReading`. Tidak ada aturan bisnis di sini.
5. **`Services/SensorService.cs`**: **file paling penting.** Di sini threshold dan logika status didefinisikan.
6. **`Services/MonitoringService.cs`**: menyusun semuanya dan mencetak tabel serta ringkasan.
7. **`Program.cs`**: hanya 8 baris. Merakit objek dan menjalankan program.
8. **`IndustrialCore.csproj`**: konfigurasi project dan instruksi menyalin CSV ke folder output.

---

## 3. Penjelasan per file

### `Models/SensorReading.cs`
Data mentah satu sensor. Satuannya ada di komentar: suhu °C, tekanan bar, getaran mm/s.

### `Models/MachineStatus.cs`
Enum dengan tiga nilai. Enum dipakai supaya typo seperti `"CRTICAL"` tidak lolos compiler.

### `Models/SensorSummary.cs`
`record` (tipe data yang tidak bisa diubah setelah dibuat). Menyimpan hasil statistik.

### `Data/SensorRepository.cs`
- `GetAll()`: membuka file, melewati header, dan memanggil `ParseLine` untuk tiap baris.
- `ParseLine()`: memastikan ada 5 kolom, lalu memanggil `ParseDecimal` untuk kolom angka.
- `ParseDecimal()`: mengubah teks menjadi angka. Kalau gagal, error menyebut nomor baris dan kolomnya.

### `Services/SensorService.cs`
- **Threshold** ada di konstanta paling atas. Kalau aturan berubah, cukup ubah di sini.
- `ValidateReading()`: menolak data dengan ID kosong atau nilai negatif.
- `CalculateAverageTemperature()`: rata-rata suhu, atau 0 kalau data kosong.
- `GetTemperatureStatus()`, `GetPressureStatus()`, `GetVibrationStatus()`: status tiap parameter.
- `GetMachineStatus()`: mengambil status **terburuk** dari ketiganya. Contoh: suhu NORMAL, tekanan WARNING, getaran CRITICAL, hasilnya CRITICAL.
- `Classify()` (private): satu-satunya tempat yang membandingkan nilai dengan batas.

Cara membacanya: mulai dari `GetMachineStatus`, lalu ikuti ke tiga method di atasnya, lalu ke `Classify`.

### `Services/MonitoringService.cs`
- `Run()`: alur utamanya. Baca → validasi → hitung → cetak.
- `BuildSummary()`: menghitung ringkasan dari daftar sensor.
- `PrintTable()` dan `PrintSummary()`: hanya urusan tampilan.

### `Program.cs`
Membuat objek satu per satu dan memanggil `Run()`. Path CSV dicari relatif terhadap lokasi file `.exe`, bukan folder tempat kamu menjalankan perintah.

### `IndustrialCore.csproj`
Baris `<None Update=... CopyToOutputDirectory=...>` memastikan `sensor_data.csv` ikut tersalin ke folder `bin/`.

---

## 4. Yang berubah dari versi aslimu

| Versi lama | Versi baru | Alasan |
|---|---|---|
| Pressure > 3.5 / > 4.5 | Pressure > 3 / > 4 | Sesuai TASK.md |
| Vibration > 4 / > 6 | Vibration > 3 / > 6 | Sesuai TASK.md |
| Satu method `GetMachineStatus` berisi semua `if` | Tiga method status + `Classify` | Task 03: jangan mengulang business rules |
| `MonitoringService` membaca CSV sendiri | `SensorRepository` yang membaca CSV | Pisahkan tanggung jawab |
| Status berupa string | Enum `MachineStatus` | Typo tidak lolos compiler |
| `Sensor.ReadingData`, `Sensor.SensorServices`, dll. | `IndustrialCore.Models`, `.Services`, `.Data` | Konsisten dengan nama folder |
| `Split` dan `Parse` langsung, crash kalau rusak | Validasi jumlah kolom dan `TryParse` dengan pesan jelas | Data rusak tidak membuat crash misterius |
| Path CSV hardcode `"Data/sensor_data.csv"` | Path dari `AppContext.BaseDirectory` | Program bisa dijalankan dari folder mana pun |
| Hanya 5 sensor terpanas | Semua sensor + ringkasan | Sesuai Task 04 |
| `_sensorService` sebagai local variable | `sensorService` | Underscore dipakai untuk field |
| File kosong `Equipment.cs`, `Sensor.cs`, `AlertService.cs` | Dihapus | Belum dipakai |
| `CsvHelper` ada di `.csproj` tapi tidak dipakai | Dilepas dari `.csproj` | Belum dipakai, dan belum bisa diuji di sini |

Detail baris per baris ada di file **`perubahan.diff`**. Buka dengan editor yang mendukung diff (VS Code, GitHub Desktop, atau `git diff --no-index`).

---

## 5. Cara menjalankan dan mengecek

```bash
dotnet build
dotnet run
```

Hasil yang seharusnya muncul:

```
Jumlah sensor      : 20
Rata-rata suhu     : 70.63 °C
Status NORMAL      : 10
Status WARNING     : 4
Status CRITICAL    : 6
```

Kalau angkanya berbeda, cek dulu apakah `sensor_data.csv` sudah benar.

---

## 6. Hal yang perlu kamu putuskan sendiri

- **Pakai CsvHelper atau tidak.** Kalau iya, ganti isi `SensorRepository.GetAll()` dengan `ClassMap`. Versi ini memakai `Split` karena paket belum bisa diuji di sini.
- **Tampilkan semua sensor atau top 5.** Di `PrintTable`, tambahkan `.OrderByDescending(r => r.Temperature).Take(5)` jika ingin top 5.
- **`double` atau `decimal`.** TASK.md meminta `double`. Versi ini memakai `decimal` karena lebih akurat untuk pengukuran. Pilih satu dan konsisten.

---

## 7. Cara cepat memahami sebuah method

Ambil `GetMachineStatus` sebagai contoh. Cukup tanyakan tiga hal:
1. **Input-nya apa?** Satu `SensorReading`.
2. **Output-nya apa?** Satu `MachineStatus`.
3. **Ia memanggil apa?** Tiga method `Get...Status`, lalu `Math.Max` untuk mengambil yang terburuk.

Kalau kamu bisa menjawab tiga pertanyaan itu untuk setiap method, kamu sudah memahami alurnya.