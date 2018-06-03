namespace TeclasMnuATeclasMnuXml
{
    internal struct DatosTecla
    {
        public string Nombre;
        public bool Control;
        public bool Mayusculas;
        public bool Alt;

        public DatosTecla(string nombre, bool control, bool may, bool alt)
        {
            Nombre = nombre;
            Control = control;
            Mayusculas = may;
            Alt = alt;
        }
    };
}
