using System.Diagnostics;
using System.Globalization;

byte failures = 0;
short anio, mes, dia;
do
{
    if (++failures > 1)
        Console.WriteLine("Año inválido, intente de nuevo...");

    Console.Write("¿En qué año naciste?: ");
} while (!short.TryParse(Console.ReadLine(), out anio)
    || anio < 1900
    || anio > DateTime.Now.Year);

failures = 0;
do
{
    if (++failures > 1)
        Console.WriteLine("Mes inválido, intente de nuevo...");

    Console.Write("¿En qué mes naciste? (1-12): ");
} while (!short.TryParse(Console.ReadLine(), out mes)
    || mes < 0
    || mes > 13);

failures = 0;
var daysInMonth = (short) DateTime.DaysInMonth(anio, mes);
do
{
    if (++failures > 1)
        Console.WriteLine("Día inválido, intente de nuevo...");

    Console.Write($"¿En qué día naciste? (1-{daysInMonth}): ");
} while (!short.TryParse(Console.ReadLine(), out dia)
    || dia < 0
    || dia > daysInMonth);

DateTime today = DateTime.Now.Date;
short totalYears = (short)(today.Year - anio);
short totalMonths;
if (mes > today.Month)
{
    --totalYears;
    totalMonths = (short)today.Month;
}
else
    totalMonths = (short)(today.Month - mes);

short totalDays;
if (dia > today.Day)
{
    --totalYears;
    totalDays = (short)(today.Day);
}
else
    totalDays = (short)(today.Day - dia);

if (totalMonths == 0 && totalDays == 0)
{
    Console.WriteLine("Feliz cumpleaños!! Cumples {0} año{1}.", 
        totalYears, totalYears > 1 || totalYears == 0 ? "s" : "");
}
else
{
    Console.WriteLine("Tu edad es: {0} año{1}, {2} mes{3} y {4} día{5}",
        totalYears, totalYears > 1 || totalYears == 0 ? "s" : "",
        totalMonths, totalMonths > 1 || totalMonths == 0 ? "es" : "",
        totalDays, totalDays > 1 || totalDays == 0 ? "s" : "");
}

var fNac = new DateTime(anio, mes, dia);
var lastDayOfYear = new DateTime(anio, 12, CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(anio, 12));
var daysSinceBornTS = lastDayOfYear - fNac;
short daysSinceBorn = (short)daysSinceBornTS.Days;
for(short i = (short)(anio + 1); i < DateTime.Now.Year; i++)
{
    daysSinceBorn += (short)CultureInfo.CurrentCulture.Calendar.GetDaysInYear(i);
}

daysSinceBorn += (short)((DateTime.Now - new DateTime(DateTime.Now.Year, 1, 1)).Days + 1);
var t1 = DateTime.Now - fNac;
Trace.Assert(t1.Days == daysSinceBorn);

t1 = (DateTime.Now - new DateTime(DateTime.Now.Year, mes, dia));

Console.WriteLine("O más precisamente: {0} años, {1} días, {2} horas, {3} minutos y {4} segundos.",
    totalYears,
    t1.Days, t1.Hours, t1.Minutes, t1.Seconds);

Console.WriteLine("La Tierra ha girado {0} veces sobre su propio eje desde el día en que naciste.", daysSinceBorn.ToString("N0"));
Console.WriteLine();
Console.ReadKey();