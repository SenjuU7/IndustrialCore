# **INDUSTRIALCORE — TASK 01**

Beginner → Intermediate

C# / .NET

Engineering Practice

## **1. Industrial scenario**

Sebuah perusahaan manufaktur memiliki beberapa mesin produksi. Setiap mesin dilengkapi sensor untuk membaca:

- Temperature (°C)
- Pressure (bar)
- Vibration (mm/s)

Sistem harus membaca data sensor, melakukan validasi, menghitung kondisi mesin, dan memberikan hasil monitoring.

**Masalah yang harus diselesaikan:**

Operator membutuhkan informasi apakah mesin berada dalam kondisi normal, warning, atau critical berdasarkan hasil pembacaan sensor.

## **2. Raw dataset**

Berikut data mentah yang akan digunakan. Simpan sebagai `sensor_data.csv`.

**sensor_data.csv**

20 records

```
sensor_id,machine_id,temperature,pressure,vibration
S001,M001,45.5,2.1,1.2
S002,M001,62.3,2.8,2.4
S003,M001,85.7,3.5,5.8
S004,M001,91.2,4.2,7.1
S005,M002,38.6,1.9,0.8
S006,M002,55.4,2.5,1.9
S007,M002,72.8,3.1,3.5
S008,M002,96.5,4.8,8.2
S009,M003,42.1,2.0,1.1
S010,M003,68.9,3.0,2.8
S011,M003,88.4,3.9,6.2
S012,M003,105.3,5.2,9.5
S013,M004,51.2,2.3,1.5
S014,M004,64.7,2.7,2.1
S015,M004,78.5,3.4,4.2
S016,M004,89.8,4.1,6.8
S017,M005,35.2,1.8,0.5
S018,M005,59.6,2.6,2.0
S019,M005,82.1,3.6,5.1
S020,M005,98.7,4.9,8.7
```

&#x20;Copy raw dataset

## **3. Business rules**

Untuk latihan ini, gunakan threshold simulasi berikut.

| Parameter   | Normal   | Warning  | Critical |
| ----------- | -------- | -------- | -------- |
| Temperature | ≤ 70°C   | 70–90°C  | > 90°C   |
| Pressure    | ≤ 3 bar  | 3–4 bar  | > 4 bar  |
| Vibration   | ≤ 3 mm/s | 3–6 mm/s | > 6 mm/s |

**Aturan klasifikasi:**

- Jika minimal satu parameter Critical → status mesin `CRITICAL`.
- Jika tidak ada Critical, tetapi minimal satu Warning → `WARNING`.
- Jika seluruh parameter Normal → `NORMAL`.

Catatan: batas tersebut merupakan aturan simulasi pembelajaran, bukan standar keselamatan mesin industri.

## **4. Engineering tasks**

Kita kerjakan bertahap. Jangan langsung membuat seluruh program.

### **Task 01 — Data Model**

Buat class `SensorReading`.

Persyaratan:

- `SensorId` bertipe string.
- `MachineId` bertipe string.
- `Temperature` bertipe double.
- `Pressure` bertipe double.
- `Vibration` bertipe double.

### **Task 02 — Method Design**

Buat class `SensorService`.

Implementasikan method berikut:

| Method                          | Return type | Tanggung jawab                  |
| ------------------------------- | ----------- | ------------------------------- |
| `ValidateReading()`             | bool        | Memeriksa validitas data        |
| `CalculateAverageTemperature()` | double      | Menghitung rata-rata temperatur |
| `GetTemperatureStatus()`        | string      | Mengklasifikasikan temperatur   |
| `GetPressureStatus()`           | string      | Mengklasifikasikan tekanan      |
| `GetVibrationStatus()`          | string      | Mengklasifikasikan getaran      |

Ketentuan validasi:

- Sensor ID tidak boleh kosong.
- Machine ID tidak boleh kosong.
- Temperature tidak boleh negatif.
- Pressure tidak boleh negatif.
- Vibration tidak boleh negatif.

### **Task 03 — Method Composition**

Buat method:

```
public string GetMachineStatus(SensorReading reading)
```

Method ini harus menggunakan tiga method klasifikasi yang sudah dibuat.

Jangan mengulang business rules di banyak tempat.

### **Task 04 — Data Processing**

Baca seluruh raw dataset dan lakukan:

1. Menghitung jumlah sensor.
2. Menghitung rata-rata temperatur.
3. Menghitung jumlah status NORMAL.
4. Menghitung jumlah status WARNING.
5. Menghitung jumlah status CRITICAL.

### **Task 05 — Output**

Format output yang diharapkan:

```
================================
     INDUSTRIALCORE MONITOR
================================

Machine ID : M001
Sensor ID  : S001

Temperature : 45.5 C
Pressure    : 2.1 bar
Vibration   : 1.2 mm/s

Machine Status : NORMAL

================================
       MONITORING SUMMARY
================================

Total Sensors : 20
Average Temp  : XX.XX C

NORMAL   : XX
WARNING  : XX
CRITICAL : XX
```

Angka `XX` harus dihitung program, bukan ditulis manual.

## **5. Struktur project**

Gunakan struktur berikut:

```
IndustrialCore/
│
├── Models/
│   └── SensorReading.cs
│
├── Services/
│   └── SensorService.cs
│
├── Data/
│   └── sensor_data.csv
│
└── Program.cs
```

## **6. Learning objectives**

Selama mengerjakan task ini, fokuskan pemahaman pada:

| Konsep                 | Pertanyaan yang harus bisa lo jawab                                         |
| ---------------------- | --------------------------------------------------------------------------- |
| Parameter              | Kenapa method menerima object `SensorReading`?                              |
| Return type            | Kenapa validasi menggunakan `bool`?                                         |
| Scope                  | Di mana sebaiknya variable `sum` dideklarasikan?                            |
| Composition            | Kenapa satu method bisa memanggil method lain?                              |
| Separation of concerns | Kenapa model dan service dipisahkan?                                        |
| Side effects           | Apa perbedaan method yang menghitung data dengan method yang mengubah data? |

## **7. Acceptance criteria**

**Engineering checklist**

## **8/8**

SensorReading berhasil dibuat.

Semua data CSV berhasil dibaca.

Validasi berhasil menangani data invalid.

Temperature, pressure, dan vibration diklasifikasikan dengan benar.

Machine status mengikuti aturan prioritas.

Average temperature dihitung secara dinamis.

Summary menampilkan jumlah setiap status.

Tidak ada business logic yang diduplikasi.

