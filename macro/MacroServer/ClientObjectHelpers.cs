public static class ClientObjectHelpers
{
    public static string StrokaZadaniya(DateTime dateTime)
    {
        string StrokaFromDate = dateTime.ToString(), Stroka="";
        int chet = 0, nechet = 0;
        for (int i = 0; i < StrokaFromDate.Length; i++)
        {
            if ("13579".Contains(StrokaFromDate[i])) nechet++;
            if ("02468".Contains(StrokaFromDate[i])) chet++;
        }
        if (chet > nechet) Stroka = "чет!";
        if (chet < nechet) Stroka = "нечет!";
        if (chet == nechet) Stroka = "равно!";
        return Stroka;
    }
}