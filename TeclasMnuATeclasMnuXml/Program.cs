﻿using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace TeclasMnuATeclasMnuXml
{
    /// <summary>
    /// Exporta un archivo clásico TECLAS.MNU al formato TECLAS.MNU.XML.
    /// </summary>
    internal class Program
    {
        private static string archivoEntrada;
        private static string archivoSalida;

        private static void Main(string[] args)
        {
            Console.WriteLine("Transformador de archivos teclas.mnu a teclas.keyboard.xml\n");

            if (!CompruebaParametros(args))
                return;


            var parámetrosXml = new XmlWriterSettings { Indent = true };

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

            var erroresLocalizados = 0;
            using (var lector = new StreamReader(archivoEntrada))
            {
                escritor.WriteStartDocument();
                escritor.WriteStartElement("Keyboard", "http://schemas.digi21.net/Digi3D/keyboard/v1.0");

                var línea = lector.ReadLine();
                while (línea != null)
                {
                    try
                    {
                        var partes = línea.Split(new[] { ' ' }, 2);

                        if (partes.Length != 2)
                            continue;

                        var teclaDigi21 = int.Parse(partes[0]);

                        if (!Diccionario.ContainsKey(teclaDigi21))
                        {
                            ++erroresLocalizados;
                            Console.WriteLine(
                                $"No ha sido posible traducir la tecla con número: {teclaDigi21} ({partes[1]})");
                            continue;
                        }

                        var datosTecla = Diccionario[teclaDigi21];

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
                        // ignored
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

            Console.WriteLine(0 == erroresLocalizados
                ? "Archivo traducido satisfactoriamente."
                : $"Se han localizado: {erroresLocalizados} errores.");
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

            archivoSalida =
                $"{Path.GetDirectoryName(archivoEntrada)}\\{Path.GetFileNameWithoutExtension(archivoEntrada)}.keyboard.xml";

            if (!File.Exists(archivoSalida)) return true;
            Console.Error.WriteLine(
                "Error: Ya existe el archivo: {0}. Este programa no sobreescribe archivos por seguridad.",
                archivoSalida);
            return false;
        }

        private static readonly Dictionary<int, DatosTecla> Diccionario = new Dictionary<int, DatosTecla>
        {
            {10193, new DatosTecla("ñ", false, false, false)},
            {10225, new DatosTecla("ñ", false, true, false)},
            {1024, new DatosTecla("3", false, false, true)},
            {10240, new DatosTecla("'", false, false, true)},
            {10363, new DatosTecla("AGUDO", true, false, true)},
            {10496, new DatosTecla("`", false, false, true)},
            {10662, new DatosTecla("`", true, false, true)},
            {10666, new DatosTecla("º", false, true, false)},
            {10682, new DatosTecla("º", false, false, false)},
            {1075, new DatosTecla("3", false, false, false)},
            {11008, new DatosTecla("#", false, false, true)},
            {11133, new DatosTecla("ç", true, false, true)},
            {11207, new DatosTecla("ç", false, false, false)},
            {11239, new DatosTecla("ç", false, true, false)},
            {11264, new DatosTecla("Z", false, false, true)},
            {11354, new DatosTecla("Z", false, false, false)},
            {11386, new DatosTecla("Z", false, true, false)},
            {11520, new DatosTecla("X", false, false, true)},
            {11608, new DatosTecla("X", false, false, false)},
            {11640, new DatosTecla("X", false, true, false)},
            {11776, new DatosTecla("C", false, false, true)},
            {11843, new DatosTecla("C", false, false, false)},
            {11875, new DatosTecla("C", false, true, false)},
            {12032, new DatosTecla("V", false, false, true)},
            {1207, new DatosTecla("3", false, true, false)},
            {12118, new DatosTecla("V", false, false, false)},
            {12150, new DatosTecla("V", false, true, false)},
            {12288, new DatosTecla("B", false, false, true)},
            {12354, new DatosTecla("B", false, false, false)},
            {12386, new DatosTecla("B", false, true, false)},
            {12544, new DatosTecla("N", false, false, true)},
            {12622, new DatosTecla("N", false, false, false)},
            {12654, new DatosTecla("N", false, true, false)},
            {1280, new DatosTecla("4", false, false, true)},
            {12800, new DatosTecla("M", false, false, true)},
            {12877, new DatosTecla("M", false, false, false)},
            {12909, new DatosTecla("M", false, true, false)},
            {12972, new DatosTecla("E", true, false, true)},
            {13056, new DatosTecla(",", false, false, true)},
            {13100, new DatosTecla(",", false, false, false)},
            {13115, new DatosTecla(",", false, true, false)},
            {1316, new DatosTecla("4", false, true, false)},
            {13312, new DatosTecla(".", false, false, true)},
            {1332, new DatosTecla("4", false, false, false)},
            {13358, new DatosTecla(".", false, false, false)},
            {13370, new DatosTecla(".", false, true, false)},
            {13568, new DatosTecla("-", true, false, true)},
            {13613, new DatosTecla("-", false, false, false)},
            {13615, new DatosTecla("TECLA DE DIVISION", false, false, false)},
            {13663, new DatosTecla("-", false, true, false)},
            {14080, new DatosTecla("NUMMULT", false, false, true)},
            {14122, new DatosTecla("TECLA DE MULTIPLICACION", false, false, false)},
            {14624, new DatosTecla("BARRA ESPACIADORA", false, false, false)},
            {15104, new DatosTecla("F1", false, false, false)},
            {1536, new DatosTecla("5", false, false, true)},
            {15360, new DatosTecla("F2", false, false, false)},
            {15616, new DatosTecla("F3", false, false, false)},
            {1573, new DatosTecla("5", false, true, false)},
            {15872, new DatosTecla("F4", false, false, false)},
            {1589, new DatosTecla("5", false, false, false)},
            {16128, new DatosTecla("F5", false, false, false)},
            {16384, new DatosTecla("F6", false, false, false)},
            {16640, new DatosTecla("F7", false, false, false)},
            {16896, new DatosTecla("F8", false, false, false)},
            {17152, new DatosTecla("F9", false, false, false)},
            {17408, new DatosTecla("F10", false, false, false)},
            {1792, new DatosTecla("6", false, false, true)},
            {17920, new DatosTecla("Break", true, false, true)},
            {18176, new DatosTecla("INICIO", false, false, false)},
            {18231, new DatosTecla("NUMERO 7", false, false, false)},
            {1830, new DatosTecla("6", false, true, false)},
            {18432, new DatosTecla("NUM 8", false, false, true)},
            {1846, new DatosTecla("6", false, false, false)},
            {18488, new DatosTecla("NUMERO 8", false, false, false)},
            {18745, new DatosTecla("NUMERO 9", false, false, false)},
            {18944, new DatosTecla("NUM SUB", false, false, true)},
            {18989, new DatosTecla("TECLA DE SUSTRACCION", false, false, false)},
            {19200, new DatosTecla("FLECHA IZQUIERDA", false, false, false)},
            {19252, new DatosTecla("NUMERO 4", false, false, false)},
            {19456, new DatosTecla("NUM 5", false, false, true)},
            {19509, new DatosTecla("NUMERO 5", false, false, false)},
            {19712, new DatosTecla("FLECHA DERECHA", false, false, false)},
            {19766, new DatosTecla("NUMERO 6", false, false, false)},
            {19968, new DatosTecla("NUM PLUS", false, false, true)},
            {20011, new DatosTecla("TECLA DE ADICION", false, false, false)},
            {20224, new DatosTecla("FIN", false, false, false)},
            {20273, new DatosTecla("NUMERO 1", false, false, false)},
            {2048, new DatosTecla("7", false, false, true)},
            {20480, new DatosTecla("FLECHA ABAJO", false, false, false)},
            {20530, new DatosTecla("NUMERO 2", false, false, false)},
            {20736, new DatosTecla("AV PAG", false, false, false)},
            {20787, new DatosTecla("NUMERO 3", false, false, false)},
            {2095, new DatosTecla("7", false, true, false)},
            {20992, new DatosTecla("INSERT", false, false, false)},
            {2103, new DatosTecla("7", false, false, false)},
            {21040, new DatosTecla("NUMERO 0", false, false, false)},
            {21248, new DatosTecla("NUM DECIMAL", false, false, true)},
            {21294, new DatosTecla("TECLA DECIMAL", false, false, false)},
            {21504, new DatosTecla("F1", false, true, false)},
            {21760, new DatosTecla("F2", false, true, false)},
            {22016, new DatosTecla("\\", false, false, true) },
            {22078, new DatosTecla("<", false, true, false)},
            {22272, new DatosTecla("F11", false, false, false)},
            {22528, new DatosTecla("F12", false, false, false)},
            {22784, new DatosTecla("F6", false, true, false)},
            {2304, new DatosTecla("8", false, false, true)},
            {23040, new DatosTecla("F7", false, true, false)},
            {23296, new DatosTecla("F8", false, true, false)},
            {2344, new DatosTecla("8", false, true, false)},
            {23552, new DatosTecla("F9", false, true, false)},
            {2360, new DatosTecla("8", false, false, false)},
            {23808, new DatosTecla("APLICACIÓN", false, false, false)},
            {24064, new DatosTecla("F1", true, false, true)},
            {24320, new DatosTecla("F2", true, false, true)},
            {24576, new DatosTecla("F3", true, false, true)},
            {24832, new DatosTecla("F4", true, false, true)},
            {25088, new DatosTecla("F5", true, false, true)},
            {25344, new DatosTecla("F6", true, false, true)},
            {256, new DatosTecla("ESC", true, false, true)},
            {2560, new DatosTecla("9", false, false, true)},
            {25600, new DatosTecla("F7", true, false, true)},
            {25856, new DatosTecla("F8", true, false, true)},
            {2601, new DatosTecla("9", false, true, false)},
            {26112, new DatosTecla("F9", true, false, true)},
            {2617, new DatosTecla("9", false, false, false)},
            {26368, new DatosTecla("F10", true, false, true)},
            {26624, new DatosTecla("F1", false, false, true)},
            {26880, new DatosTecla("F2", false, false, true)},

            {27136, new DatosTecla("AV PAG", false, true, false)},
            {27392, new DatosTecla("FLECHA ARRIBA", true, false, true)},
            {27648, new DatosTecla("F5", false, false, true)},
            {27904, new DatosTecla("F6", false, false, true)},
            {2816, new DatosTecla("0", false, false, true)},
            {28160, new DatosTecla("F7", false, false, true)},
            {283, new DatosTecla("ESC", false, false, false)},
            {28416, new DatosTecla("F8", false, false, true)},
            {2864, new DatosTecla("0", false, false, false)},
            {28672, new DatosTecla("F11", false, true, false)},
            {2877, new DatosTecla("0", false, true, false)},
            {28928, new DatosTecla("F10", false, false, true)},
            {29440, new DatosTecla("FLECHA ABAJO", true, false, true)},
            {29696, new DatosTecla("AV PAG", true, false, true)},
            {29952, new DatosTecla("INSERT", true, false, true)},
            {30208, new DatosTecla("PGUP", false, false, true)},
            {3072, new DatosTecla("-", false, false, true)},
            {30720, new DatosTecla("LEFT", false, false, true)},
            {3111, new DatosTecla("'", false, false, false)},
            {31232, new DatosTecla("F11", true, false, true)},
            {3135, new DatosTecla("'", false, true, false)},
            {31488, new DatosTecla("F12", true, false, true)},
            {31744, new DatosTecla("END", false, false, true)},
            {32000, new DatosTecla("DOWN", false, false, true)},
            {32256, new DatosTecla("PGDOWN", false, false, true)},
            {32512, new DatosTecla("INSERT", false, false, true)},
            {32768, new DatosTecla("DELETE", false, false, true)},
            {3328, new DatosTecla("=", false, false, true)},
            {33792, new DatosTecla("F11", false, false, true)},
            {34048, new DatosTecla("F12", false, false, true)},
            {3489, new DatosTecla("¡", false, false, false)},
            {3519, new DatosTecla("¡", false, true, false)},
            {3584, new DatosTecla("BACKSPACE", false, false, true)},
            {3592, new DatosTecla("RETROCESO", false, false, false)},
            {3849, new DatosTecla("TABULACION", false, false, false)},
            {4096, new DatosTecla("Q", false, false, true)},
            {4209, new DatosTecla("Q", false, false, false)},
            {4352, new DatosTecla("W", false, false, true)},
            {4471, new DatosTecla("W", false, false, false)},
            {4608, new DatosTecla("E", false, false, true)},
            {4709, new DatosTecla("E", false, false, false)},
            {4864, new DatosTecla("R", false, false, true)},
            {4978, new DatosTecla("R", false, false, false)},
            {512, new DatosTecla("1", false, false, true)},
            {5120, new DatosTecla("T", false, false, true)},
            {5236, new DatosTecla("T", false, false, false)},
            {5376, new DatosTecla("Y", false, false, true)},
            {545, new DatosTecla("1", false, true, false)},
            {5497, new DatosTecla("Y", false, false, false)},
            {561, new DatosTecla("1", false, false, false)},
            {5632, new DatosTecla("U", false, false, true)},
            {5749, new DatosTecla("U", false, false, false)},
            {5888, new DatosTecla("I", false, false, true)},
            {5993, new DatosTecla("I", false, false, false)},
            {6144, new DatosTecla("O", false, false, true)},
            {6255, new DatosTecla("O", false, false, false)},
            {6400, new DatosTecla("P", false, false, true)},
            {6512, new DatosTecla("P", false, false, false)},
            {6656, new DatosTecla("[", false, false, true)},
            {6747, new DatosTecla("GRAVE", true, false, true)},
            {6912, new DatosTecla("]", false, false, true)},
            {6954, new DatosTecla("+", false, true, false)},
            {6955, new DatosTecla("+", false, false, false)},
            {7005, new DatosTecla("+", true, false, true)},
            {7168, new DatosTecla("ENTER", false, false, true)},
            {7181, new DatosTecla("ENTRAR", false, false, false)},
            {768, new DatosTecla("2", false, false, true)},
            {7680, new DatosTecla("A", false, false, true)},
            {7745, new DatosTecla("A", false, false, false)},
            {7777, new DatosTecla("A", false, true, false)},
            {7936, new DatosTecla("S", false, false, true)},
            {8019, new DatosTecla("S", false, false, false)},
            {802, new DatosTecla("2", false, true, false)},
            {8051, new DatosTecla("S", false, true, false)},
            {818, new DatosTecla("2", false, false, false)},
            {8192, new DatosTecla("D", false, false, true)},
            {8260, new DatosTecla("D", false, false, false)},
            {8292, new DatosTecla("D", false, true, false)},
            {8448, new DatosTecla("F", false, false, true)},
            {8518, new DatosTecla("F", false, false, false)},
            {8550, new DatosTecla("F", false, true, false)},
            {8704, new DatosTecla("G", false, false, true)},
            {8775, new DatosTecla("G", false, false, false)},
            {8807, new DatosTecla("G", false, true, false)},
            {8960, new DatosTecla("H", false, false, true)},
            {9032, new DatosTecla("H", false, false, false)},
            {9064, new DatosTecla("H", false, true, false)},
            {9216, new DatosTecla("J", false, false, true)},
            {9290, new DatosTecla("J", false, false, false)},
            {9322, new DatosTecla("J", false, true, false)},
            {9472, new DatosTecla("K", false, false, true)},
            {9547, new DatosTecla("K", false, false, false)},
            {9579, new DatosTecla("K", false, true, false)},
            {9644, new DatosTecla("4", true, false, true)},
            {9728, new DatosTecla("L", false, false, true)},
            {9804, new DatosTecla("L", false, false, false)},
            {9836, new DatosTecla("L", false, true, false)},
            {9984, new DatosTecla(";", false, false, true)}
        };
    }
}
