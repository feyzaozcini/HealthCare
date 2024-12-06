using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.TestValueRanges;

public enum TestUnitTypes
{
    [Display(Name = "mg/dL")]  // Miligram/desilitre
    MgPerDl = 1,

    [Display(Name = "g/dL")]   // Gram/desilitre
    GPerDl = 2,

    [Display(Name = "mmol/L")] // Milimol/litre
    MmolPerL = 3,

    [Display(Name = "μmol/L")] // Mikromol/litre
    UmolPerL = 4,

    [Display(Name = "mcg/dL")] // Mikrogram/desilitre
    McgPerDl = 5,

    [Display(Name = "ng/mL")]  // Nanogram/mililitre
    NgPerMl = 6,

    [Display(Name = "pg/mL")]  // Pikogram/mililitre
    PgPerMl = 7,

    [Display(Name = "IU/L")]   // Uluslararası birim/litre
    IUPerL = 8,

    [Display(Name = "U/L")]    // Birim/litre
    UPerL = 9,

    [Display(Name = "mmHg")]   // Milimetre cıva
    MmHg = 10,

    [Display(Name = "kPa")]    // Kilopascal
    KPa = 11,

    [Display(Name = "fL")]     // Femtolitre
    Fl = 12,

    [Display(Name = "mL")]     // Mililitre
    Ml = 13,

    [Display(Name = "L")]      // Litre
    L = 14,

    [Display(Name = "mg")]     // Miligram
    Mg = 15,

    [Display(Name = "g")]      // Gram
    G = 16,

    [Display(Name = "mcg")]    // Mikrogram
    Mcg = 17,

    [Display(Name = "ng")]     // Nanogram
    Ng = 18,

    [Display(Name = "pg")]     // Pikogram
    Pg = 19,

    [Display(Name = "%")]      // Yüzde
    Percent = 20,

    [Display(Name = "/mL")]    // Mililitre başına
    PerMl = 21,

    [Display(Name = "/mm³")]   // Milimetreküp başına
    PerMm3 = 22,

    [Display(Name = "/μL")]    // Mikrolitre başına
    PerMicroliter = 23,

    [Display(Name = "sec")]    // Saniye
    Sec = 24,

    [Display(Name = "min")]    // Dakika
    Min = 25,

    [Display(Name = "hr")]     // Saat
    Hr = 26,

    [Display(Name = "Ratio")]  // Oran
    Ratio = 27,

    [Display(Name = "AU")]     // Rastgele birim (Arbitrary Unit)
    ArbitraryUnit = 28
}
