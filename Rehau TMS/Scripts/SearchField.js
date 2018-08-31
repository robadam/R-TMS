﻿function myFunction(x)
{
    var input, filter, table, tr, td, i;
    input = document.getElementById("Input");
    filter = input.value.toUpperCase();
    table = document.getElementById("Table");
    tr = table.getElementsByTagName("tr");
    for (i = 0; i < tr.length; i++)
    {
        td = tr[i].getElementsByTagName("td")[x];
        if (td)
        {
            if (td.innerHTML.toUpperCase().indexOf(filter) > -1)
            {
                tr[i].style.display = "";
            }
            else
            {
                tr[i].style.display = "none";
            }
        }
    }
}