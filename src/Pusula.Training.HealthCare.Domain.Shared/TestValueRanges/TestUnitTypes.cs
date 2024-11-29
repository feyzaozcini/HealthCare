using System.ComponentModel;

namespace Pusula.Training.HealthCare.TestValueRanges;

public enum TestUnitTypes
{
    [Description("mg/dL")]  // Miligram/desilitre
    MgPerDl = 1,

    [Description("g/dL")]   // Gram/desilitre
    GPerDl = 2,

    [Description("mmol/L")] // Milimol/litre
    MmolPerL = 3,

    [Description("μmol/L")] // Mikromol/litre
    UmolPerL = 4,

    [Description("mcg/dL")] // Mikrogram/desilitre
    McgPerDl = 5,

    [Description("ng/mL")]  // Nanogram/mililitre
    NgPerMl = 6,

    [Description("pg/mL")]  // Pikogram/mililitre
    PgPerMl = 7,

    [Description("IU/L")]   // Uluslararası birim/litre
    IUPerL = 8,

    [Description("U/L")]    // Birim/litre
    UPerL = 9,

    [Description("mmHg")]   // Milimetre cıva
    MmHg = 10,

    [Description("kPa")]    // Kilopascal
    KPa = 11,

    [Description("fL")]     // Femtolitre
    Fl = 12,

    [Description("mL")]     // Mililitre
    Ml = 13,

    [Description("L")]      // Litre
    L = 14,

    [Description("mg")]     // Miligram
    Mg = 15,

    [Description("g")]      // Gram
    G = 16,

    [Description("mcg")]    // Mikrogram
    Mcg = 17,

    [Description("ng")]     // Nanogram
    Ng = 18,

    [Description("pg")]     // Pikogram
    Pg = 19,

    [Description("%")]      // Yüzde
    Percent = 20,

    [Description("/mL")]    // Mililitre başına
    PerMl = 21,

    [Description("/mm³")]   // Milimetreküp başına
    PerMm3 = 22,

    [Description("/μL")]    // Mikrolitre başına
    PerMicroliter = 23,

    [Description("sec")]    // Saniye
    Sec = 24,

    [Description("min")]    // Dakika
    Min = 25,

    [Description("hr")]     // Saat
    Hr = 26,

    [Description("Ratio")]  // Oran
    Ratio = 27,

    [Description("AU")]     // Rastgele birim (Arbitrary Unit)
    ArbitraryUnit = 28
}
