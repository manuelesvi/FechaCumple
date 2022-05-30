using System.Diagnostics;
using System.Globalization;

byte failures = 0;
short anio, mes, dia;
var cal = CultureInfo.CurrentCulture.Calendar;

do
{
    if (failures++ > 0)
        Console.WriteLine("Año inválido, intente de nuevo...");

    Console.Write("¿En qué año naciste?: ");
} while (!short.TryParse(Console.ReadLine(), out anio)
    || anio < 1900
    || anio > DateTime.Now.Year);

failures = 0;
do
{
    if (failures++ > 0)
        Console.WriteLine("Mes inválido, intente de nuevo...");

    Console.Write("¿En qué mes naciste? (1-12): ");
} while (!short.TryParse(Console.ReadLine(), out mes)
    || mes < 0
    || mes >= 13);

failures = 0;
var daysInMonth = (short) DateTime.DaysInMonth(anio, mes);
do
{
    if (failures++ > 0)
        Console.WriteLine("Día inválido, intente de nuevo...");

    Console.Write($"¿En qué día naciste? (1-{daysInMonth}): ");
} while (!short.TryParse(Console.ReadLine(), out dia)
    || dia < 0
    || dia > daysInMonth);

var fNac = new DateTime(anio, mes, dia);
DateTime today = DateTime.Now.Date;

if (fNac > today)
    throw new ApplicationException("Fecha futura! Aùn no has nacido.");

short totalYears = (short)(today.Year - anio);
short totalDays;
if (mes > today.Month || (mes == today.Month && dia > today.Day))
{
    // birthday is ahead
    --totalYears;    
    totalDays = (short)today.DayOfYear;
}
else
{
    // birthday passed, calculate days old
    totalDays = (short)(today - new DateTime(today.Year, mes, dia)).Days;
}

if (totalDays == 0)
{
    // happy b-day
    Console.WriteLine("Feliz cumpleaños!! Cumples {0} año{1}.", 
        totalYears, totalYears > 1 || totalYears == 0 ? "s" : "");
}
else
{
    Console.WriteLine("Tu edad es: {0} año{1} y {2} día{3}",
        totalYears, totalYears > 1 || totalYears == 0 ? "s" : "",        
        totalDays, totalDays > 1 || totalDays == 0 ? "s" : "");
}

short daysSinceBorn;
if (anio < DateTime.Now.Year)
{
    var lastDayOfYear = new DateTime(anio, 12, cal.GetDaysInMonth(anio, 12));
    var daysSinceBornTS = lastDayOfYear - fNac;
    daysSinceBorn = (short)daysSinceBornTS.Days;
    for (short i = (short)(anio + 1); i < DateTime.Now.Year; i++)
    {
        daysSinceBorn += (short)cal.GetDaysInYear(i);
    }
    daysSinceBorn += (short)((DateTime.Now - new DateTime(DateTime.Now.Year, 1, 1)).Days + 1);
}
else
    daysSinceBorn = (short) (DateTime.Today - fNac).Days;

var t1 = DateTime.Now - fNac;
Trace.Assert(t1.Days == daysSinceBorn);

var thisYearBirthday = new DateTime(DateTime.Now.Year, mes, dia);
if (thisYearBirthday < today)
    t1 = (DateTime.Now - thisYearBirthday);
else
    t1 = TimeSpan.FromDays(today.DayOfYear) + DateTime.Now.TimeOfDay;

Console.WriteLine("O más precisamente: {0} años, {1} días, {2} horas, {3} minutos y {4} segundos.",
    totalYears,
    t1.Days, t1.Hours, t1.Minutes, t1.Seconds);

Console.WriteLine("La Tierra ha girado {0} veces sobre su propio eje desde el día en que naciste.", daysSinceBorn.ToString("N0"));
Console.WriteLine();
Console.ReadKey();