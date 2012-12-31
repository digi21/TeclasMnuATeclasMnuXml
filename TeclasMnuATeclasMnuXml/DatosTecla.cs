using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeclasMnuATeclasMnuXml
{
    struct DatosTecla
    {
        public string Nombre;
        public bool Control;
        public bool Mayusculas;
        public bool Alt;

        public DatosTecla(string nombre, bool control, bool may, bool alt)
        {
            this.Nombre = nombre;
            this.Control = control;
            this.Mayusculas = may;
            this.Alt = alt;
        }
    };
}
