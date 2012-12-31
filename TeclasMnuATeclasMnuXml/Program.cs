using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace TeclasMnuATeclasMnuXml
{
    /// <summary>
    /// Exporta un archivo clásico TECLAS.MNU al formato TECLAS.MNU.XML.
    /// </summary>
    class Program
    {
        private static string archivoEntrada;
        private static string archivoSalida;

        static void Main(string[] args)
        {
            Console.WriteLine("Transformador de archivos teclas.mnu a teclas.keyboard.xml\n");

            if (!CompruebaParametros(args))
                return;


            XmlWriterSettings parámetrosXml = new XmlWriterSettings();
            parámetrosXml.Indent = true;

            XmlWriter escritor;
            try
            {
                escritor = XmlWriter.Create(archivoSalida, parámetrosXml);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Error: No se ha podido crear el archivo XML");
                Console.Error.WriteLine(e.Message);
                return;
            }

            int erroresLocalizados = 0;
            using (StreamReader lector = new StreamReader(archivoEntrada))
            {
                escritor.WriteStartDocument();
                escritor.WriteStartElement("Keyboard", "http://schemas.digi21.net/Digi3D/keyboard/v1.0");

                string línea = lector.ReadLine();
                while (línea != null)
                {
                    try
                    {
                        string[] partes = línea.Split(new[] {' '}, 2);

                        if (partes.Length != 2)
                            continue;

                        int teclaDigi21 = int.Parse(partes[0]);

                        if (!diccionario.ContainsKey(teclaDigi21))
                        {
                            ++erroresLocalizados;
                            Console.WriteLine(string.Format("No ha sido posible traducir la tecla con número: {0} ({1})",
                                teclaDigi21,
                                partes[1]));
                            continue;
                        }

                        DatosTecla datosTecla = diccionario[teclaDigi21];

                        escritor.WriteStartElement("Key");
                        escritor.WriteAttributeString("Name", datosTecla.Nombre);
                        if (datosTecla.Control)
                            escritor.WriteAttributeString("Control", "true");
                        if (datosTecla.Mayusculas)
                            escritor.WriteAttributeString("Shift", "true");
                        if (datosTecla.Alt)
                            escritor.WriteAttributeString("Menu", "true");

                        escritor.WriteString(partes[1]);
                        escritor.WriteEndElement();
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        línea = lector.ReadLine();
                    }
                }
            }
            escritor.WriteEndElement();
            escritor.WriteEndDocument();
            escritor.Flush();
            escritor.Close();

            if (0 == erroresLocalizados)
                Console.WriteLine("Archivo traducido satisfactoriamente.");
            else
                Console.WriteLine(string.Format("Se han localizado: {0} errores.", erroresLocalizados));
        }

        private static bool CompruebaParametros(string[] args)
        {
            if (args.Length == 0)
            {
                Console.Error.WriteLine("Uso: TECLASMNUATECLASMNUXML archivo.mnu");
                return false;
            }

            archivoEntrada = args[0];
            if (!File.Exists(archivoEntrada))
            {
                Console.Error.WriteLine("Error: No se ha localizado el archivo: {0}",
                    archivoEntrada);
                return false;
            }

            archivoSalida = string.Format("{0}\\{1}.keyboard.xml",
                Path.GetDirectoryName(archivoEntrada),
                Path.GetFileNameWithoutExtension(archivoEntrada));

            if (File.Exists(archivoSalida))
            {
                Console.Error.WriteLine("Error: Ya existe el archivo: {0}. Este programa no sobreescribe archivos por seguridad.",
                    archivoSalida);
                return false;
            }
            return true;
        }

        private static Dictionary<int, DatosTecla> diccionario = new Dictionary<int, DatosTecla>
        {
	        { 283, new DatosTecla("ESC", false, false, false) },
	        { 15104, new DatosTecla("F1", false, false, false) },
	        { 15360, new DatosTecla("F2", false, false, false) },
	        { 15616, new DatosTecla("F3", false, false, false) },
	        { 15872, new DatosTecla("F4", false, false, false) },
	        { 16128, new DatosTecla("F5", false, false, false) },
	        { 16384, new DatosTecla("F6", false, false, false) },
	        { 16640, new DatosTecla("F7", false, false, false) },
	        { 16896, new DatosTecla("F8", false, false, false) },
	        { 17152, new DatosTecla("F9", false, false, false) },
	        { 17408, new DatosTecla("F10", false, false, false) },
	        { 22272, new DatosTecla("F11", false, false, false) },
	        { 22528, new DatosTecla("F12", false, false, false) },
	        { 17920, new DatosTecla("BLOQ DESPL", false, false, false) },
	        { 17664, new DatosTecla("PAUSA", false, false, false) },
	        { 2877, new DatosTecla("0", false, true, false) },
	        { 2344, new DatosTecla("8", false, true, false) },
	        { 2601, new DatosTecla("9", false, true, false) },
	        { 3592, new DatosTecla("RETROCESO", false, false, false) },
	        { 10682, new DatosTecla("º", false, false, false) },
	        { 561, new DatosTecla("1", false, false, false) },
	        { 818, new DatosTecla("2", false, false, false) },
	        { 1075, new DatosTecla("3", false, false, false) },
	        { 1332, new DatosTecla("4", false, false, false) },
	        { 1589, new DatosTecla("5", false, false, false) },
	        { 1846, new DatosTecla("6", false, false, false) },
	        { 2103, new DatosTecla("7", false, false, false) },
	        { 2360, new DatosTecla("8", false, false, false) },
	        { 2617, new DatosTecla("9", false, false, false) },
	        { 2864, new DatosTecla("0", false, false, false) },
	        { 3111, new DatosTecla("'", false, false, false) },
	        { 3489, new DatosTecla("¡", false, false, false) },
	        { 20992, new DatosTecla("INSERT", false, false, false) },
	        { 18176, new DatosTecla("INICIO", false, false, false) },
	        { 18688, new DatosTecla("RE PAG", false, false, false) },
	        { 13615, new DatosTecla("TECLA DE DIVISION", false, false, false) },
	        { 14122, new DatosTecla("TECLA DE MULTIPLICACION", false, false, false) },
	        { 18989, new DatosTecla("TECLA DE SUSTRACCION", false, false, false) },
	        { 20011, new DatosTecla("TECLA DE ADICION", false, false, false) },
	        { 18745, new DatosTecla("NUMERO 9", false, false, false) },
	        { 18488, new DatosTecla("NUMERO 8", false, false, false) },
	        { 18231, new DatosTecla("NUMERO 7", false, false, false) },
	        { 20736, new DatosTecla("AV PAG", false, false, false) },
	        { 20224, new DatosTecla("FIN", false, false, false) },
	        { 21248, new DatosTecla("SUPR", false, false, false) },
	        { 7181, new DatosTecla("ENTRAR", false, false, false) },
	        { 6955, new DatosTecla("+", false, false, false) },
	        { 6656, new DatosTecla("GRAVE", false, false, false) },
	        { 6496, new DatosTecla("P", false, false, false) },
	        { 6255, new DatosTecla("O", false, false, false) },
	        { 5993, new DatosTecla("I", false, false, false) },
	        { 5749, new DatosTecla("U", false, false, false) },
	        { 5497, new DatosTecla("Y", false, false, false) },
	        { 5236, new DatosTecla("T", false, false, false) },
	        { 4978, new DatosTecla("R", false, false, false) },
	        { 4709, new DatosTecla("E", false, false, false) },
	        { 4471, new DatosTecla("W", false, false, false) },
	        { 4209, new DatosTecla("Q", false, false, false) },
	        { 3849, new DatosTecla("TABULACION", false, false, false) },
	        { 14848, new DatosTecla("BLOQ MAYUS", false, false, false) },
	        { 7777, new DatosTecla("A", false, false, false) },
	        { 8051, new DatosTecla("S", false, false, false) },
	        { 8292, new DatosTecla("D", false, false, false) },
	        { 8550, new DatosTecla("F", false, false, false) },
	        { 8807, new DatosTecla("G", false, false, false) },
	        { 9064, new DatosTecla("H", false, false, false) },
	        { 9322, new DatosTecla("J", false, false, false) },
	        { 9579, new DatosTecla("K", false, false, false) },
	        { 9836, new DatosTecla("L", false, false, false) },
	        { 10225, new DatosTecla("ñ", false, false, false) },
	        { 10240, new DatosTecla("AGUDO", false, false, false) },
	        { 11188, new DatosTecla("ç", false, false, false) },
	        { 19252, new DatosTecla("NUMERO 4", false, false, false) },
	        { 19509, new DatosTecla("NUMERO 5", false, false, false) },
	        { 19766, new DatosTecla("NUMERO 6", false, false, false) },
	        { 20787, new DatosTecla("NUMERO 3", false, false, false) },
	        { 20530, new DatosTecla("NUMERO 2", false, false, false) },
	        { 20273, new DatosTecla("NUMERO 1", false, false, false) },
	        { 18432, new DatosTecla("FLECHA ARRIBA", false, false, false) },
	        { 13613, new DatosTecla("-", false, false, false) },
	        { 13358, new DatosTecla(".", false, false, false) },
	        { 13100, new DatosTecla(",", false, false, false) },
	        { 12909, new DatosTecla("M", false, false, false) },
	        { 12654, new DatosTecla("N", false, false, false) },
	        { 12386, new DatosTecla("B", false, false, false) },
	        { 12150, new DatosTecla("V", false, false, false) },
	        { 11875, new DatosTecla("C", false, false, false) },
	        { 11640, new DatosTecla("X", false, false, false) },
	        { 11386, new DatosTecla("Z", false, false, false) },
	        { 22076, new DatosTecla("<", false, false, false) },
	        { 23296, new DatosTecla("WINDOWS IZQUIERDA", false, false, false) },
	        { 25856, new DatosTecla("ALT", false, false, true) },
	        { 14624, new DatosTecla("BARRA ESPACIADORA", false, false, false) },
	        { 23808, new DatosTecla("APLICACIÓN", false, false, false) },
	        { 19200, new DatosTecla("FLECHA IZQUIERDA", false, false, false) },
	        { 20480, new DatosTecla("FLECHA ABAJO", false, false, false) },
	        { 19712, new DatosTecla("FLECHA DERECHA", false, false, false) },
	        { 21040, new DatosTecla("NUMERO 0", false, false, false) },
	        { 21294, new DatosTecla("TECLA DECIMAL", false, false, false) },
	        { 24064, new DatosTecla("F1", true, false, false) },
	        { 24320, new DatosTecla("F2", true, false, false) },
	        { 24576, new DatosTecla("F3", true, false, false) },
	        { 24832, new DatosTecla("F4", true, false, false) },
	        { 25088, new DatosTecla("F5", true, false, false) },
	        { 25344, new DatosTecla("F6", true, false, false) },
	        { 25600, new DatosTecla("F7", true, false, false) },
	        { 26112, new DatosTecla("F9", true, false, false) },
	        { 26368, new DatosTecla("F10", true, false, false) },
	        { 31232, new DatosTecla("F11", true, false, false) },
	        { 31488, new DatosTecla("F12", true, false, false) },
	        { 17923, new DatosTecla("BLOQ DESPL", true, false, false) },
	        { 8704, new DatosTecla("8", true, true, false) },
	        { 8960, new DatosTecla("9", true, true, false) },
	        { 3711, new DatosTecla("RETROCESO", true, false, false) },
	        { 27904, new DatosTecla("TECLA DE SUSTRACCION", true, false, false) },
	        { 23040, new DatosTecla("TECLA DE MULTIPLICACION", true, false, false) },
	        { 26624, new DatosTecla("BLOQ NUM", true, false, false) },
	        { 27648, new DatosTecla("RE PAG", true, false, false) },
	        { 27136, new DatosTecla("INICIO", true, false, false) },
	        { 29952, new DatosTecla("INSERT", true, false, false) },
	        { 12288, new DatosTecla("¡", true, false, false) },
	        { 12032, new DatosTecla("'", true, false, false) },
	        { 11776, new DatosTecla("0", true, false, false) },
	        { 11520, new DatosTecla("9", true, false, false) },
	        { 11264, new DatosTecla("8", true, false, false) },
	        { 11008, new DatosTecla("7", true, false, false) },
	        { 10752, new DatosTecla("6", true, false, false) },
	        { 10496, new DatosTecla("5", true, false, false) },
	        { 9984, new DatosTecla("3", true, false, false) },
	        { 9728, new DatosTecla("2", true, false, false) },
	        { 9472, new DatosTecla("1", true, false, false) },
	        { 19456, new DatosTecla("º", true, false, false) },
	        { 12800, new DatosTecla("TABULACION", true, false, false) },
	        { 4113, new DatosTecla("Q", true, false, false) },
	        { 4375, new DatosTecla("W", true, false, false) },
	        { 4613, new DatosTecla("E", true, false, false) },
	        { 4882, new DatosTecla("R", true, false, false) },
	        { 5140, new DatosTecla("T", true, false, false) },
	        { 5401, new DatosTecla("Y", true, false, false) },
	        { 5653, new DatosTecla("U", true, false, false) },
	        { 5897, new DatosTecla("I", true, false, false) },
	        { 6159, new DatosTecla("O", true, false, false) },
	        { 6416, new DatosTecla("P", true, false, false) },
	        { 6683, new DatosTecla("GRAVE", true, false, false) },
	        { 6941, new DatosTecla("+", true, false, false) },
	        { 7178, new DatosTecla("ENTRAR", true, false, false) },
	        { 30208, new DatosTecla("SUPR", true, false, false) },
	        { 29184, new DatosTecla("FIN", true, false, false) },
	        { 29696, new DatosTecla("AV PAG", true, false, false) },
	        { 27392, new DatosTecla("NUMERO 8", true, false, false) },
	        { 28928, new DatosTecla("TECLA DE ADICION", true, false, false) },
	        { 28672, new DatosTecla("NUMERO 6", true, false, false) },
	        { 28416, new DatosTecla("NUMERO 5", true, false, false) },
	        { 28160, new DatosTecla("NUMERO 4", true, false, false) },
	        { 11036, new DatosTecla("ç", true, false, false) },
	        { 18944, new DatosTecla("ñ", true, false, false) },
	        { 9740, new DatosTecla("L", true, false, false) },
	        { 9483, new DatosTecla("K", true, false, false) },
	        { 9226, new DatosTecla("J", true, false, false) },
	        { 8968, new DatosTecla("H", true, false, false) },
	        { 8711, new DatosTecla("G", true, false, false) },
	        { 8454, new DatosTecla("F", true, false, false) },
	        { 8196, new DatosTecla("D", true, false, false) },
	        { 7955, new DatosTecla("S", true, false, false) },
	        { 7681, new DatosTecla("A", true, false, false) },
	        { 22044, new DatosTecla("<", true, false, false) },
	        { 11290, new DatosTecla("Z", true, false, false) },
	        { 11544, new DatosTecla("X", true, false, false) },
	        { 11779, new DatosTecla("C", true, false, false) },
	        { 12054, new DatosTecla("V", true, false, false) },
	        { 12290, new DatosTecla("B", true, false, false) },
	        { 12558, new DatosTecla("N", true, false, false) },
	        { 12813, new DatosTecla("M", true, false, false) },
	        { 22016, new DatosTecla(",", true, false, false) },
	        { 29440, new DatosTecla("NUMERO 2", true, false, false) },
	        { 32768, new DatosTecla("APLICACIÓN", true, false, false) },
	        { 32256, new DatosTecla("WINDOWS IZQUIERDA", true, false, false) },
	        { 21504, new DatosTecla("F1", false, true, false) },
	        { 21760, new DatosTecla("F2", false, true, false) },
	        { 22784, new DatosTecla("F6", false, true, false) },
	        { 23552, new DatosTecla("F9", false, true, false) },
	        { 3519, new DatosTecla("¡", false, true, false) },
	        { 3135, new DatosTecla("'", false, true, false) },
	        { 2095, new DatosTecla("7", false, true, false) },
	        { 1830, new DatosTecla("6", false, true, false) },
	        { 1573, new DatosTecla("5", false, true, false) },
	        { 1316, new DatosTecla("4", false, true, false) },
	        { 1207, new DatosTecla("3", false, true, false) },
	        { 802, new DatosTecla("2", false, true, false) },
	        { 545, new DatosTecla("1", false, true, false) },
	        { 10666, new DatosTecla("º", false, true, false) },
	        { 4177, new DatosTecla("Q", false, true, false) },
	        { 4439, new DatosTecla("W", false, true, false) },
	        { 4677, new DatosTecla("E", false, true, false) },
	        { 4946, new DatosTecla("R", false, true, false) },
	        { 5204, new DatosTecla("T", false, true, false) },
	        { 5465, new DatosTecla("Y", false, true, false) },
	        { 5717, new DatosTecla("U", false, true, false) },
	        { 5961, new DatosTecla("I", false, true, false) },
	        { 6223, new DatosTecla("O", false, true, false) },
	        { 6480, new DatosTecla("P", false, true, false) },
	        { 13056, new DatosTecla("GRAVE", false, true, false) },
	        { 7006, new DatosTecla("+", false, true, false) },
	        { 11207, new DatosTecla("ç", false, true, false) },
	        { 10152, new DatosTecla("ñ", false, true, false) },
	        { 9804, new DatosTecla("L", false, true, false) },
	        { 9547, new DatosTecla("K", false, true, false) },
	        { 9290, new DatosTecla("J", false, true, false) },
	        { 9032, new DatosTecla("H", false, true, false) },
	        { 8775, new DatosTecla("G", false, true, false) },
	        { 8518, new DatosTecla("F", false, true, false) },
	        { 8260, new DatosTecla("D", false, true, false) },
	        { 8019, new DatosTecla("S", false, true, false) },
	        { 7745, new DatosTecla("A", false, true, false) },
	        { 22078, new DatosTecla("<", false, true, false) },
	        { 11354, new DatosTecla("Z", false, true, false) },
	        { 11608, new DatosTecla("X", false, true, false) },
	        { 11843, new DatosTecla("C", false, true, false) },
	        { 12118, new DatosTecla("V", false, true, false) },
	        { 12354, new DatosTecla("B", false, true, false) },
	        { 12622, new DatosTecla("N", false, true, false) },
	        { 12877, new DatosTecla("M", false, true, false) },
	        { 13115, new DatosTecla(",", false, true, false) },
	        { 13370, new DatosTecla(".", false, true, false) },
	        { 13663, new DatosTecla("-", false, true, false) },
	        { 26880, new DatosTecla("FLECHA ABAJO", false, true, false) },
	        { 14336, new DatosTecla("Y", true, false, true) },
	        { 9216, new DatosTecla("0", false, true, true) },
	        { 30464, new DatosTecla("TECLA DE SUSTRACCION", false, false, true) },
	        { 32512, new DatosTecla("INSERT", false, false, true) },
	        { 14592, new DatosTecla("'", false, false, true) },
	        { 14080, new DatosTecla("9", false, false, true) },
	        { 13824, new DatosTecla("8", false, false, true) },
	        { 13568, new DatosTecla("7", false, false, true) },
	        { 13312, new DatosTecla("6", false, false, true) },
	        { 12544, new DatosTecla("3", false, false, true) },
	        { 31744, new DatosTecla("FIN", false, false, true) },
	        { 30976, new DatosTecla("NUMERO 5", false, false, true) },
	        { 30720, new DatosTecla("NUMERO 4", false, false, true) },
	        { 19968, new DatosTecla("F", false, false, true) },
	        { 33536, new DatosTecla("<", false, false, true) },
	        { 32000, new DatosTecla("NUMERO 2", false, false, true) }
        };
    }
}
