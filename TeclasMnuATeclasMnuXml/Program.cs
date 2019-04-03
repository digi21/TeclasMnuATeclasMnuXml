using System;
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

                        foreach (var datosTecla in Diccionario[teclaDigi21])
                        {
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

        private static readonly Dictionary<int, List<DatosTecla>> Diccionario = new Dictionary<int, List<DatosTecla>>   {{
    283, new List<DatosTecla> {
        new DatosTecla("ESC", false, false, false),
        new DatosTecla("ESC", false, true, false),
    }},
{
    512, new List<DatosTecla> {
        new DatosTecla("1", true, false, false),
        new DatosTecla("1", false, false, true),
        new DatosTecla("1", true, true, false),
        new DatosTecla("1", false, true, true),
    }},
{
    545, new List<DatosTecla> {
        new DatosTecla("1", false, true, false),
    }},
{
    561, new List<DatosTecla> {
        new DatosTecla("1", false, false, false),
    }},
{
    636, new List<DatosTecla> {
        new DatosTecla("1", true, false, true),
    }},
{
    768, new List<DatosTecla> {
        new DatosTecla("2", true, false, false),
        new DatosTecla("2", false, false, true),
        new DatosTecla("2", true, true, false),
        new DatosTecla("2", false, true, true),
    }},
{
    802, new List<DatosTecla> {
        new DatosTecla("2", false, true, false),
    }},
{
    818, new List<DatosTecla> {
        new DatosTecla("2", false, false, false),
    }},
{
    832, new List<DatosTecla> {
        new DatosTecla("2", true, false, true),
    }},
{
    1024, new List<DatosTecla> {
        new DatosTecla("3", true, false, false),
        new DatosTecla("3", false, false, true),
        new DatosTecla("3", true, true, false),
        new DatosTecla("3", false, true, true),
    }},
{
    1075, new List<DatosTecla> {
        new DatosTecla("3", false, false, false),
    }},
{
    1150, new List<DatosTecla> {
        new DatosTecla("3", true, false, true),
    }},
{
    1207, new List<DatosTecla> {
        new DatosTecla("3", false, true, false),
    }},
{
    1280, new List<DatosTecla> {
        new DatosTecla("4", true, false, false),
        new DatosTecla("4", false, false, true),
        new DatosTecla("4", true, false, true),
        new DatosTecla("4", true, true, false),
        new DatosTecla("4", false, true, true),
    }},
{
    1316, new List<DatosTecla> {
        new DatosTecla("4", false, true, false),
    }},
{
    1332, new List<DatosTecla> {
        new DatosTecla("4", false, false, false),
    }},
{
    1536, new List<DatosTecla> {
        new DatosTecla("5", true, false, false),
        new DatosTecla("5", false, false, true),
        new DatosTecla("5", true, true, false),
        new DatosTecla("5", false, true, true),
    }},
{
    1573, new List<DatosTecla> {
        new DatosTecla("5", false, true, false),
    }},
{
    1589, new List<DatosTecla> {
        new DatosTecla("5", false, false, false),
    }},
{
    1792, new List<DatosTecla> {
        new DatosTecla("6", true, false, false),
        new DatosTecla("6", false, false, true),
        new DatosTecla("6", false, true, true),
    }},
{
    1822, new List<DatosTecla> {
        new DatosTecla("6", true, true, false),
    }},
{
    1830, new List<DatosTecla> {
        new DatosTecla("6", false, true, false),
    }},
{
    1846, new List<DatosTecla> {
        new DatosTecla("6", false, false, false),
    }},
{
    1964, new List<DatosTecla> {
        new DatosTecla("6", true, false, true),
    }},
{
    2048, new List<DatosTecla> {
        new DatosTecla("7", true, false, false),
        new DatosTecla("7", false, false, true),
        new DatosTecla("7", true, false, true),
        new DatosTecla("7", true, true, false),
        new DatosTecla("7", false, true, true),
    }},
{
    2095, new List<DatosTecla> {
        new DatosTecla("7", false, true, false),
    }},
{
    2103, new List<DatosTecla> {
        new DatosTecla("7", false, false, false),
    }},
{
    2304, new List<DatosTecla> {
        new DatosTecla("8", true, false, false),
        new DatosTecla("8", false, false, true),
        new DatosTecla("8", true, false, true),
        new DatosTecla("8", true, true, false),
        new DatosTecla("8", false, true, true),
    }},
{
    2344, new List<DatosTecla> {
        new DatosTecla("8", false, true, false),
    }},
{
    2360, new List<DatosTecla> {
        new DatosTecla("8", false, false, false),
    }},
{
    2560, new List<DatosTecla> {
        new DatosTecla("9", true, false, false),
        new DatosTecla("9", false, false, true),
        new DatosTecla("9", true, false, true),
        new DatosTecla("9", true, true, false),
        new DatosTecla("9", false, true, true),
    }},
{
    2601, new List<DatosTecla> {
        new DatosTecla("9", false, true, false),
    }},
{
    2617, new List<DatosTecla> {
        new DatosTecla("9", false, false, false),
    }},
{
    2816, new List<DatosTecla> {
        new DatosTecla("0", true, false, false),
        new DatosTecla("0", false, false, true),
        new DatosTecla("0", true, false, true),
        new DatosTecla("0", false, true, true),
    }},
{
    2864, new List<DatosTecla> {
        new DatosTecla("0", false, false, false),
    }},
{
    2877, new List<DatosTecla> {
        new DatosTecla("0", false, true, false),
    }},
{
    3072, new List<DatosTecla> {
        new DatosTecla("'", true, false, false),
        new DatosTecla("'", false, false, true),
        new DatosTecla("'", true, false, true),
        new DatosTecla("'", true, true, false),
        new DatosTecla("'", false, true, true),
    }},
{
    3111, new List<DatosTecla> {
        new DatosTecla("'", false, false, false),
    }},
{
    3135, new List<DatosTecla> {
        new DatosTecla("'", false, true, false),
    }},
{
    3328, new List<DatosTecla> {
        new DatosTecla("¡", true, false, false),
        new DatosTecla("¡", false, false, true),
        new DatosTecla("¡", true, false, true),
        new DatosTecla("¡", true, true, false),
        new DatosTecla("¡", false, true, true),
    }},
{
    3489, new List<DatosTecla> {
        new DatosTecla("¡", false, false, false),
    }},
{
    3519, new List<DatosTecla> {
        new DatosTecla("¡", false, true, false),
    }},
{
    3584, new List<DatosTecla> {
        new DatosTecla("RETROCESO", false, false, true),
        new DatosTecla("RETROCESO", true, false, true),
        new DatosTecla("RETROCESO", true, true, false),
    }},
{
    3592, new List<DatosTecla> {
        new DatosTecla("RETROCESO", false, false, false),
        new DatosTecla("RETROCESO", false, true, false),
    }},
{
    3711, new List<DatosTecla> {
        new DatosTecla("RETROCESO", true, false, false),
    }},
{
    3840, new List<DatosTecla> {
        new DatosTecla("TABULACION", true, false, false),
        new DatosTecla("TABULACION", true, true, false),
    }},
{
    3849, new List<DatosTecla> {
        new DatosTecla("TABULACION", false, false, false),
        new DatosTecla("TABULACION", false, true, false),
    }},
{
    4096, new List<DatosTecla> {
        new DatosTecla("Q", false, false, true),
        new DatosTecla("Q", true, false, true),
        new DatosTecla("Q", false, true, true),
    }},
{
    4113, new List<DatosTecla> {
        new DatosTecla("Q", true, false, false),
        new DatosTecla("Q", true, true, false),
    }},
{
    4177, new List<DatosTecla> {
        new DatosTecla("Q", false, true, false),
    }},
{
    4209, new List<DatosTecla> {
        new DatosTecla("Q", false, false, false),
    }},
{
    4352, new List<DatosTecla> {
        new DatosTecla("W", false, false, true),
        new DatosTecla("W", true, false, true),
        new DatosTecla("W", false, true, true),
    }},
{
    4375, new List<DatosTecla> {
        new DatosTecla("W", true, false, false),
        new DatosTecla("W", true, true, false),
    }},
{
    4439, new List<DatosTecla> {
        new DatosTecla("W", false, true, false),
    }},
{
    4471, new List<DatosTecla> {
        new DatosTecla("W", false, false, false),
    }},
{
    4608, new List<DatosTecla> {
        new DatosTecla("E", false, false, true),
        new DatosTecla("E", false, true, true),
    }},
{
    4613, new List<DatosTecla> {
        new DatosTecla("E", true, false, false),
        new DatosTecla("E", true, true, false),
    }},
{
    4677, new List<DatosTecla> {
        new DatosTecla("E", false, true, false),
    }},
{
    4709, new List<DatosTecla> {
        new DatosTecla("E", false, false, false),
    }},
{
    4864, new List<DatosTecla> {
        new DatosTecla("R", false, false, true),
        new DatosTecla("R", true, false, true),
        new DatosTecla("R", false, true, true),
    }},
{
    4882, new List<DatosTecla> {
        new DatosTecla("R", true, false, false),
        new DatosTecla("R", true, true, false),
    }},
{
    4946, new List<DatosTecla> {
        new DatosTecla("R", false, true, false),
    }},
{
    4978, new List<DatosTecla> {
        new DatosTecla("R", false, false, false),
    }},
{
    5120, new List<DatosTecla> {
        new DatosTecla("T", false, false, true),
        new DatosTecla("T", true, false, true),
        new DatosTecla("T", false, true, true),
    }},
{
    5140, new List<DatosTecla> {
        new DatosTecla("T", true, false, false),
        new DatosTecla("T", true, true, false),
    }},
{
    5204, new List<DatosTecla> {
        new DatosTecla("T", false, true, false),
    }},
{
    5236, new List<DatosTecla> {
        new DatosTecla("T", false, false, false),
    }},
{
    5376, new List<DatosTecla> {
        new DatosTecla("Y", false, false, true),
        new DatosTecla("Y", true, false, true),
        new DatosTecla("Y", false, true, true),
    }},
{
    5401, new List<DatosTecla> {
        new DatosTecla("Y", true, false, false),
        new DatosTecla("Y", true, true, false),
    }},
{
    5465, new List<DatosTecla> {
        new DatosTecla("Y", false, true, false),
    }},
{
    5497, new List<DatosTecla> {
        new DatosTecla("Y", false, false, false),
    }},
{
    5632, new List<DatosTecla> {
        new DatosTecla("U", false, false, true),
        new DatosTecla("U", true, false, true),
        new DatosTecla("U", false, true, true),
    }},
{
    5653, new List<DatosTecla> {
        new DatosTecla("U", true, false, false),
        new DatosTecla("U", true, true, false),
    }},
{
    5717, new List<DatosTecla> {
        new DatosTecla("U", false, true, false),
    }},
{
    5749, new List<DatosTecla> {
        new DatosTecla("U", false, false, false),
    }},
{
    5888, new List<DatosTecla> {
        new DatosTecla("I", false, false, true),
        new DatosTecla("I", true, false, true),
        new DatosTecla("I", false, true, true),
    }},
{
    5897, new List<DatosTecla> {
        new DatosTecla("I", true, false, false),
        new DatosTecla("I", true, true, false),
    }},
{
    5961, new List<DatosTecla> {
        new DatosTecla("I", false, true, false),
    }},
{
    5993, new List<DatosTecla> {
        new DatosTecla("I", false, false, false),
    }},
{
    6144, new List<DatosTecla> {
        new DatosTecla("O", false, false, true),
        new DatosTecla("O", true, false, true),
        new DatosTecla("O", false, true, true),
    }},
{
    6159, new List<DatosTecla> {
        new DatosTecla("O", true, false, false),
        new DatosTecla("O", true, true, false),
    }},
{
    6223, new List<DatosTecla> {
        new DatosTecla("O", false, true, false),
    }},
{
    6255, new List<DatosTecla> {
        new DatosTecla("O", false, false, false),
    }},
{
    6400, new List<DatosTecla> {
        new DatosTecla("P", false, false, true),
        new DatosTecla("P", true, false, true),
        new DatosTecla("P", false, true, true),
    }},
{
    6416, new List<DatosTecla> {
        new DatosTecla("P", true, false, false),
        new DatosTecla("P", true, true, false),
    }},
{
    6480, new List<DatosTecla> {
        new DatosTecla("P", false, true, false),
    }},
{
    6496, new List<DatosTecla> {
        new DatosTecla("P", false, false, false),
    }},
{
    6656, new List<DatosTecla> {
        new DatosTecla("GRAVE", false, false, false),
        new DatosTecla("GRAVE", false, true, false),
        new DatosTecla("GRAVE", false, false, true),
        new DatosTecla("GRAVE", true, true, false),
        new DatosTecla("GRAVE", false, true, true),
    }},
{
    6683, new List<DatosTecla> {
        new DatosTecla("GRAVE", true, false, false),
    }},
{
    6747, new List<DatosTecla> {
        new DatosTecla("GRAVE", true, false, true),
    }},
{
    6912, new List<DatosTecla> {
        new DatosTecla("+", false, false, true),
        new DatosTecla("+", true, true, false),
        new DatosTecla("+", false, true, true),
    }},
{
    6941, new List<DatosTecla> {
        new DatosTecla("+", true, false, false),
    }},
{
    6955, new List<DatosTecla> {
        new DatosTecla("+", false, false, false),
    }},
{
    7005, new List<DatosTecla> {
        new DatosTecla("+", true, false, true),
    }},
{
    7006, new List<DatosTecla> {
        new DatosTecla("+", false, true, false),
    }},
{
    7168, new List<DatosTecla> {
        new DatosTecla("ENTRAR", false, false, true),
        new DatosTecla("INTRO", false, false, true),
        new DatosTecla("ENTRAR", true, false, true),
        new DatosTecla("INTRO", true, false, true),
        new DatosTecla("ENTRAR", true, true, false),
        new DatosTecla("INTRO", true, true, false),
        new DatosTecla("Num Enter", false, true, true),
    }},
{
    7178, new List<DatosTecla> {
        new DatosTecla("ENTRAR", true, false, false),
        new DatosTecla("INTRO", true, false, false),
    }},
{
    7181, new List<DatosTecla> {
        new DatosTecla("ENTRAR", false, false, false),
        new DatosTecla("INTRO", false, false, false),
        new DatosTecla("ENTRAR", false, true, false),
        new DatosTecla("INTRO", false, true, false),
    }},
{
    7680, new List<DatosTecla> {
        new DatosTecla("A", false, false, true),
        new DatosTecla("A", true, false, true),
        new DatosTecla("A", false, true, true),
    }},
{
    7681, new List<DatosTecla> {
        new DatosTecla("A", true, false, false),
        new DatosTecla("A", true, true, false),
    }},
{
    7745, new List<DatosTecla> {
        new DatosTecla("A", false, true, false),
    }},
{
    7777, new List<DatosTecla> {
        new DatosTecla("A", false, false, false),
    }},
{
    7936, new List<DatosTecla> {
        new DatosTecla("S", false, false, true),
        new DatosTecla("S", true, false, true),
        new DatosTecla("S", false, true, true),
    }},
{
    7955, new List<DatosTecla> {
        new DatosTecla("S", true, false, false),
        new DatosTecla("S", true, true, false),
    }},
{
    8019, new List<DatosTecla> {
        new DatosTecla("S", false, true, false),
    }},
{
    8051, new List<DatosTecla> {
        new DatosTecla("S", false, false, false),
    }},
{
    8192, new List<DatosTecla> {
        new DatosTecla("D", false, false, true),
        new DatosTecla("D", true, false, true),
        new DatosTecla("D", false, true, true),
    }},
{
    8196, new List<DatosTecla> {
        new DatosTecla("D", true, false, false),
        new DatosTecla("D", true, true, false),
    }},
{
    8260, new List<DatosTecla> {
        new DatosTecla("D", false, true, false),
    }},
{
    8292, new List<DatosTecla> {
        new DatosTecla("D", false, false, false),
    }},
{
    8448, new List<DatosTecla> {
        new DatosTecla("F", false, false, true),
        new DatosTecla("F", true, false, true),
        new DatosTecla("F", false, true, true),
    }},
{
    8454, new List<DatosTecla> {
        new DatosTecla("F", true, false, false),
        new DatosTecla("F", true, true, false),
    }},
{
    8518, new List<DatosTecla> {
        new DatosTecla("F", false, true, false),
    }},
{
    8550, new List<DatosTecla> {
        new DatosTecla("F", false, false, false),
    }},
{
    8704, new List<DatosTecla> {
        new DatosTecla("G", false, false, true),
        new DatosTecla("G", true, false, true),
        new DatosTecla("G", false, true, true),
    }},
{
    8711, new List<DatosTecla> {
        new DatosTecla("G", true, false, false),
        new DatosTecla("G", true, true, false),
    }},
{
    8775, new List<DatosTecla> {
        new DatosTecla("G", false, true, false),
    }},
{
    8807, new List<DatosTecla> {
        new DatosTecla("G", false, false, false),
    }},
{
    8960, new List<DatosTecla> {
        new DatosTecla("H", false, false, true),
        new DatosTecla("H", true, false, true),
        new DatosTecla("H", false, true, true),
    }},
{
    8968, new List<DatosTecla> {
        new DatosTecla("H", true, false, false),
        new DatosTecla("H", true, true, false),
    }},
{
    9032, new List<DatosTecla> {
        new DatosTecla("H", false, true, false),
    }},
{
    9064, new List<DatosTecla> {
        new DatosTecla("H", false, false, false),
    }},
{
    9216, new List<DatosTecla> {
        new DatosTecla("J", false, false, true),
        new DatosTecla("J", true, false, true),
        new DatosTecla("J", false, true, true),
    }},
{
    9226, new List<DatosTecla> {
        new DatosTecla("J", true, false, false),
        new DatosTecla("J", true, true, false),
    }},
{
    9290, new List<DatosTecla> {
        new DatosTecla("J", false, true, false),
    }},
{
    9322, new List<DatosTecla> {
        new DatosTecla("J", false, false, false),
    }},
{
    9472, new List<DatosTecla> {
        new DatosTecla("K", false, false, true),
        new DatosTecla("K", true, false, true),
        new DatosTecla("K", false, true, true),
    }},
{
    9483, new List<DatosTecla> {
        new DatosTecla("K", true, false, false),
        new DatosTecla("K", true, true, false),
    }},
{
    9547, new List<DatosTecla> {
        new DatosTecla("K", false, true, false),
    }},
{
    9579, new List<DatosTecla> {
        new DatosTecla("K", false, false, false),
    }},
{
    9728, new List<DatosTecla> {
        new DatosTecla("L", false, false, true),
        new DatosTecla("L", true, false, true),
        new DatosTecla("L", false, true, true),
    }},
{
    9740, new List<DatosTecla> {
        new DatosTecla("L", true, false, false),
        new DatosTecla("L", true, true, false),
    }},
{
    9804, new List<DatosTecla> {
        new DatosTecla("L", false, true, false),
    }},
{
    9836, new List<DatosTecla> {
        new DatosTecla("L", false, false, false),
    }},
{
    9900, new List<DatosTecla> {
        new DatosTecla("5", true, false, true),
    }},
{
    9984, new List<DatosTecla> {
        new DatosTecla("ñ", true, false, false),
        new DatosTecla("ñ", false, false, true),
        new DatosTecla("ñ", true, false, true),
        new DatosTecla("ñ", true, true, false),
        new DatosTecla("ñ", false, true, true),
    }},
{
    10152, new List<DatosTecla> {
        new DatosTecla("ñ", false, true, false),
    }},
{
    10225, new List<DatosTecla> {
        new DatosTecla("ñ", false, false, false),
    }},
{
    10240, new List<DatosTecla> {
        new DatosTecla("AGUDO", false, false, false),
        new DatosTecla("AGUDO", false, true, false),
        new DatosTecla("AGUDO", true, false, false),
        new DatosTecla("AGUDO", false, false, true),
        new DatosTecla("AGUDO", true, true, false),
        new DatosTecla("AGUDO", false, true, true),
    }},
{
    10363, new List<DatosTecla> {
        new DatosTecla("AGUDO", true, false, true),
    }},
{
    10496, new List<DatosTecla> {
        new DatosTecla("º", true, false, false),
        new DatosTecla("º", false, false, true),
        new DatosTecla("º", true, true, false),
        new DatosTecla("º", false, true, true),
    }},
{
    10588, new List<DatosTecla> {
        new DatosTecla("º", true, false, true),
    }},
{
    10666, new List<DatosTecla> {
        new DatosTecla("º", false, true, false),
    }},
{
    10682, new List<DatosTecla> {
        new DatosTecla("º", false, false, false),
    }},
{
    11008, new List<DatosTecla> {
        new DatosTecla("ç", false, false, true),
        new DatosTecla("ç", true, true, false),
        new DatosTecla("ç", false, true, true),
    }},
{
    11036, new List<DatosTecla> {
        new DatosTecla("ç", true, false, false),
    }},
{
    11133, new List<DatosTecla> {
        new DatosTecla("ç", true, false, true),
    }},
{
    11188, new List<DatosTecla> {
        new DatosTecla("ç", false, false, false),
    }},
{
    11207, new List<DatosTecla> {
        new DatosTecla("ç", false, true, false),
    }},
{
    11264, new List<DatosTecla> {
        new DatosTecla("Z", false, false, true),
        new DatosTecla("Z", true, false, true),
        new DatosTecla("Z", false, true, true),
    }},
{
    11290, new List<DatosTecla> {
        new DatosTecla("Z", true, false, false),
        new DatosTecla("Z", true, true, false),
    }},
{
    11354, new List<DatosTecla> {
        new DatosTecla("Z", false, true, false),
    }},
{
    11386, new List<DatosTecla> {
        new DatosTecla("Z", false, false, false),
    }},
{
    11520, new List<DatosTecla> {
        new DatosTecla("X", false, false, true),
        new DatosTecla("X", true, false, true),
        new DatosTecla("X", false, true, true),
    }},
{
    11544, new List<DatosTecla> {
        new DatosTecla("X", true, false, false),
        new DatosTecla("X", true, true, false),
    }},
{
    11608, new List<DatosTecla> {
        new DatosTecla("X", false, true, false),
    }},
{
    11640, new List<DatosTecla> {
        new DatosTecla("X", false, false, false),
    }},
{
    11776, new List<DatosTecla> {
        new DatosTecla("C", false, false, true),
        new DatosTecla("C", true, false, true),
        new DatosTecla("C", false, true, true),
    }},
{
    11779, new List<DatosTecla> {
        new DatosTecla("C", true, false, false),
        new DatosTecla("C", true, true, false),
    }},
{
    11843, new List<DatosTecla> {
        new DatosTecla("C", false, true, false),
    }},
{
    11875, new List<DatosTecla> {
        new DatosTecla("C", false, false, false),
    }},
{
    12032, new List<DatosTecla> {
        new DatosTecla("V", false, false, true),
        new DatosTecla("V", true, false, true),
        new DatosTecla("V", false, true, true),
    }},
{
    12054, new List<DatosTecla> {
        new DatosTecla("V", true, false, false),
        new DatosTecla("V", true, true, false),
    }},
{
    12118, new List<DatosTecla> {
        new DatosTecla("V", false, true, false),
    }},
{
    12150, new List<DatosTecla> {
        new DatosTecla("V", false, false, false),
    }},
{
    12288, new List<DatosTecla> {
        new DatosTecla("B", false, false, true),
        new DatosTecla("B", true, false, true),
        new DatosTecla("B", false, true, true),
    }},
{
    12290, new List<DatosTecla> {
        new DatosTecla("B", true, false, false),
        new DatosTecla("B", true, true, false),
    }},
{
    12354, new List<DatosTecla> {
        new DatosTecla("B", false, true, false),
    }},
{
    12386, new List<DatosTecla> {
        new DatosTecla("B", false, false, false),
    }},
{
    12544, new List<DatosTecla> {
        new DatosTecla("N", false, false, true),
        new DatosTecla("N", true, false, true),
        new DatosTecla("N", false, true, true),
    }},
{
    12558, new List<DatosTecla> {
        new DatosTecla("N", true, false, false),
        new DatosTecla("N", true, true, false),
    }},
{
    12622, new List<DatosTecla> {
        new DatosTecla("N", false, true, false),
    }},
{
    12654, new List<DatosTecla> {
        new DatosTecla("N", false, false, false),
    }},
{
    12800, new List<DatosTecla> {
        new DatosTecla("M", false, false, true),
        new DatosTecla("M", true, false, true),
        new DatosTecla("M", false, true, true),
    }},
{
    12813, new List<DatosTecla> {
        new DatosTecla("M", true, false, false),
        new DatosTecla("M", true, true, false),
    }},
{
    12877, new List<DatosTecla> {
        new DatosTecla("M", false, true, false),
    }},
{
    12909, new List<DatosTecla> {
        new DatosTecla("M", false, false, false),
    }},
{
    12972, new List<DatosTecla> {
        new DatosTecla("E", true, false, true),
    }},
{
    13056, new List<DatosTecla> {
        new DatosTecla(",", true, false, false),
        new DatosTecla(",", false, false, true),
        new DatosTecla(",", true, false, true),
        new DatosTecla(",", true, true, false),
        new DatosTecla(",", false, true, true),
    }},
{
    13100, new List<DatosTecla> {
        new DatosTecla(",", false, false, false),
    }},
{
    13115, new List<DatosTecla> {
        new DatosTecla(",", false, true, false),
    }},
{
    13312, new List<DatosTecla> {
        new DatosTecla(".", true, false, false),
        new DatosTecla(".", false, false, true),
        new DatosTecla(".", true, false, true),
        new DatosTecla(".", true, true, false),
        new DatosTecla(".", false, true, true),
    }},
{
    13358, new List<DatosTecla> {
        new DatosTecla(".", false, false, false),
    }},
{
    13370, new List<DatosTecla> {
        new DatosTecla(".", false, true, false),
    }},
{
    13568, new List<DatosTecla> {
        new DatosTecla("-", true, false, false),
        new DatosTecla("TECLA DE DIVISION", true, false, false),
        new DatosTecla("-", false, false, true),
        new DatosTecla("TECLA DE DIVISION", false, false, true),
        new DatosTecla("-", true, false, true),
        new DatosTecla("TECLA DE DIVISION", true, false, true),
        new DatosTecla("-", true, true, false),
        new DatosTecla("TECLA DE DIVISION", true, true, false),
        new DatosTecla("-", false, true, true),
        new DatosTecla("TECLA DE DIVISION", false, true, true),
    }},
{
    13613, new List<DatosTecla> {
        new DatosTecla("-", false, false, false),
    }},
{
    13615, new List<DatosTecla> {
        new DatosTecla("TECLA DE DIVISION", false, false, false),
        new DatosTecla("TECLA DE DIVISION", false, true, false),
    }},
{
    13663, new List<DatosTecla> {
        new DatosTecla("-", false, true, false),
    }},
{
    14080, new List<DatosTecla> {
        new DatosTecla("TECLA DE MULTIPLICACION", true, false, false),
        new DatosTecla("TECLA DE MULTIPLICACION", false, false, true),
        new DatosTecla("TECLA DE MULTIPLICACION", true, false, true),
        new DatosTecla("TECLA DE MULTIPLICACION", true, true, false),
        new DatosTecla("TECLA DE MULTIPLICACION", false, true, true),
    }},
{
    14122, new List<DatosTecla> {
        new DatosTecla("TECLA DE MULTIPLICACION", false, false, false),
        new DatosTecla("TECLA DE MULTIPLICACION", false, true, false),
    }},
{
    14624, new List<DatosTecla> {
        new DatosTecla("BARRA ESPACIADORA", false, false, false),
        new DatosTecla("BARRA ESPACIADORA", false, true, false),
        new DatosTecla("BARRA ESPACIADORA", true, false, false),
    }},
{
    15104, new List<DatosTecla> {
        new DatosTecla("F1", false, false, false),
    }},
{
    15360, new List<DatosTecla> {
        new DatosTecla("F2", false, false, false),
    }},
{
    15616, new List<DatosTecla> {
        new DatosTecla("F3", false, false, false),
    }},
{
    15872, new List<DatosTecla> {
        new DatosTecla("F4", false, false, false),
    }},
{
    16128, new List<DatosTecla> {
        new DatosTecla("F5", false, false, false),
    }},
{
    16384, new List<DatosTecla> {
        new DatosTecla("F6", false, false, false),
    }},
{
    16640, new List<DatosTecla> {
        new DatosTecla("F7", false, false, false),
    }},
{
    16896, new List<DatosTecla> {
        new DatosTecla("F8", false, false, false),
    }},
{
    17152, new List<DatosTecla> {
        new DatosTecla("F9", false, false, false),
    }},
{
    17408, new List<DatosTecla> {
        new DatosTecla("F10", false, false, false),
    }},
{
    18176, new List<DatosTecla> {
        new DatosTecla("INICIO", false, false, false),
        new DatosTecla("NUMERO 7", false, false, false),
        new DatosTecla("NUMERO 7", true, false, false),
        new DatosTecla("NUMERO 7", false, false, true),
        new DatosTecla("NUMERO 7", true, false, true),
    }},
{
    18231, new List<DatosTecla> {
        new DatosTecla("NUMERO 7", false, false, false),
    }},
{
    18432, new List<DatosTecla> {
        new DatosTecla("FLECHA ARRIBA", false, false, false),
        new DatosTecla("NUMERO 8", false, false, false),
        new DatosTecla("NUMERO 8", true, false, false),
        new DatosTecla("NUMERO 8", false, false, true),
        new DatosTecla("NUMERO 8", true, false, true),
    }},
{
    18488, new List<DatosTecla> {
        new DatosTecla("NUMERO 8", false, false, false),
    }},
{
    18688, new List<DatosTecla> {
        new DatosTecla("RE PAG", false, false, false),
        new DatosTecla("NUMERO 9", false, false, false),
        new DatosTecla("NUMERO 9", true, false, false),
        new DatosTecla("NUMERO 9", false, false, true),
        new DatosTecla("NUMERO 9", true, false, true),
    }},
{
    18745, new List<DatosTecla> {
        new DatosTecla("NUMERO 9", false, false, false),
    }},
{
    18944, new List<DatosTecla> {
        new DatosTecla("TECLA DE SUSTRACCION", true, false, false),
        new DatosTecla("TECLA DE SUSTRACCION", false, false, true),
        new DatosTecla("TECLA DE SUSTRACCION", true, false, true),
        new DatosTecla("TECLA DE SUSTRACCION", true, true, false),
        new DatosTecla("TECLA DE SUSTRACCION", false, true, true),
    }},
{
    18989, new List<DatosTecla> {
        new DatosTecla("TECLA DE SUSTRACCION", false, false, false),
        new DatosTecla("TECLA DE SUSTRACCION", false, true, false),
    }},
{
    19200, new List<DatosTecla> {
        new DatosTecla("FLECHA IZQUIERDA", false, false, false),
        new DatosTecla("NUMERO 4", false, false, false),
        new DatosTecla("NUMERO 4", true, false, false),
        new DatosTecla("NUMERO 4", false, false, true),
        new DatosTecla("NUMERO 4", true, false, true),
    }},
{
    19252, new List<DatosTecla> {
        new DatosTecla("NUMERO 4", false, false, false),
    }},
{
    19456, new List<DatosTecla> {
        new DatosTecla("NUMERO 5", false, false, false),
        new DatosTecla("NUMERO 5", true, false, false),
        new DatosTecla("NUMERO 5", false, false, true),
        new DatosTecla("NUMERO 5", true, false, true),
        new DatosTecla("Num 5", false, false, true),
    }},
{
    19509, new List<DatosTecla> {
        new DatosTecla("NUMERO 5", false, false, false),
    }},
{
    19712, new List<DatosTecla> {
        new DatosTecla("FLECHA DERECHA", false, false, false),
        new DatosTecla("NUMERO 6", false, false, false),
        new DatosTecla("NUMERO 6", true, false, false),
        new DatosTecla("NUMERO 6", false, false, true),
        new DatosTecla("NUMERO 6", true, false, true),
    }},
{
    19766, new List<DatosTecla> {
        new DatosTecla("NUMERO 6", false, false, false),
    }},
{
    19968, new List<DatosTecla> {
        new DatosTecla("TECLA DE ADICION", true, false, false),
        new DatosTecla("TECLA DE ADICION", false, false, true),
        new DatosTecla("TECLA DE ADICION", true, false, true),
        new DatosTecla("TECLA DE ADICION", true, true, false),
        new DatosTecla("TECLA DE ADICION", false, true, true),
    }},
{
    20011, new List<DatosTecla> {
        new DatosTecla("TECLA DE ADICION", false, false, false),
        new DatosTecla("TECLA DE ADICION", false, true, false),
    }},
{
    20224, new List<DatosTecla> {
        new DatosTecla("FIN", false, false, false),
        new DatosTecla("NUMERO 1", false, false, false),
        new DatosTecla("NUMERO 1", false, false, true),
        new DatosTecla("NUMERO 1", true, false, true),
    }},
{
    20273, new List<DatosTecla> {
        new DatosTecla("NUMERO 1", false, false, false),
    }},
{
    20480, new List<DatosTecla> {
        new DatosTecla("FLECHA ABAJO", false, false, false),
        new DatosTecla("NUMERO 2", false, false, false),
        new DatosTecla("NUMERO 2", true, false, false),
        new DatosTecla("NUMERO 2", false, false, true),
        new DatosTecla("NUMERO 2", true, false, true),
    }},
{
    20530, new List<DatosTecla> {
        new DatosTecla("NUMERO 2", false, false, false),
    }},
{
    20736, new List<DatosTecla> {
        new DatosTecla("AV PAG", false, false, false),
        new DatosTecla("NUMERO 3", false, false, false),
        new DatosTecla("NUMERO 3", true, false, false),
        new DatosTecla("NUMERO 3", false, false, true),
        new DatosTecla("NUMERO 3", true, false, true),
    }},
{
    20787, new List<DatosTecla> {
        new DatosTecla("NUMERO 3", false, false, false),
    }},
{
    20992, new List<DatosTecla> {
        new DatosTecla("INSERT", false, false, false),
        new DatosTecla("NUMERO 0", false, false, false),
        new DatosTecla("NUMERO 0", true, false, false),
        new DatosTecla("NUMERO 0", false, false, true),
        new DatosTecla("NUMERO 0", true, false, true),
        new DatosTecla("BARRA ESPACIADORA", true, true, false),
        new DatosTecla("BARRA ESPACIADORA", false, true, true),
    }},
{
    21040, new List<DatosTecla> {
        new DatosTecla("NUMERO 0", false, false, false),
    }},
{
    21248, new List<DatosTecla> {
        new DatosTecla("SUPR", false, false, false),
        new DatosTecla("TECLA DECIMAL", false, false, false),
        new DatosTecla("TECLA DECIMAL", true, false, false),
        new DatosTecla("TECLA DECIMAL", false, false, true),
    }},
{
    21294, new List<DatosTecla> {
        new DatosTecla("TECLA DECIMAL", false, false, false),
    }},
{
    21504, new List<DatosTecla> {
        new DatosTecla("F1", false, true, false),
        new DatosTecla("F1", true, true, false),
        new DatosTecla("F1", false, true, true),
    }},
{
    21760, new List<DatosTecla> {
        new DatosTecla("F2", false, true, false),
        new DatosTecla("F2", true, true, false),
        new DatosTecla("F2", false, true, true),
    }},
{
    22016, new List<DatosTecla> {
        new DatosTecla("F3", false, true, false),
        new DatosTecla("<", false, false, true),
        new DatosTecla("<", true, false, true),
        new DatosTecla("F3", true, true, false),
        new DatosTecla("<", true, true, false),
        new DatosTecla("F3", false, true, true),
        new DatosTecla("<", false, true, true),
    }},
{
    22044, new List<DatosTecla> {
        new DatosTecla("<", true, false, false),
    }},
{
    22076, new List<DatosTecla> {
        new DatosTecla("<", false, false, false),
    }},
{
    22078, new List<DatosTecla> {
        new DatosTecla("<", false, true, false),
    }},
{
    22272, new List<DatosTecla> {
        new DatosTecla("F11", false, false, false),
        new DatosTecla("F4", false, true, false),
        new DatosTecla("F4", true, true, false),
        new DatosTecla("F4", false, true, true),
    }},
{
    22528, new List<DatosTecla> {
        new DatosTecla("F12", false, false, false),
        new DatosTecla("F5", false, true, false),
        new DatosTecla("F5", true, true, false),
    }},
{
    22784, new List<DatosTecla> {
        new DatosTecla("F6", false, true, false),
        new DatosTecla("F6", true, true, false),
        new DatosTecla("F6", false, true, true),
    }},
{
    23040, new List<DatosTecla> {
        new DatosTecla("F7", false, true, false),
        new DatosTecla("F7", true, true, false),
        new DatosTecla("F7", false, true, true),
    }},
{
    23296, new List<DatosTecla> {
        new DatosTecla("F8", false, true, false),
        new DatosTecla("F8", true, true, false),
        new DatosTecla("F8", false, true, true),
    }},
{
    23552, new List<DatosTecla> {
        new DatosTecla("F9", false, true, false),
        new DatosTecla("BARRA ESPACIADORA", true, false, true),
        new DatosTecla("F9", true, true, false),
        new DatosTecla("F9", false, true, true),
    }},
{
    23808, new List<DatosTecla> {
        new DatosTecla("F10", false, true, false),
        new DatosTecla("F10", true, true, false),
        new DatosTecla("F10", false, true, true),
    }},
{
    24064, new List<DatosTecla> {
        new DatosTecla("F1", true, false, false),
        new DatosTecla("F1", true, false, true),
    }},
{
    24320, new List<DatosTecla> {
        new DatosTecla("F2", true, false, false),
        new DatosTecla("F2", true, false, true),
    }},
{
    24576, new List<DatosTecla> {
        new DatosTecla("INICIO", false, true, false),
        new DatosTecla("F3", true, false, false),
        new DatosTecla("F3", true, false, true),
        new DatosTecla("INICIO", true, true, false),
    }},
{
    24832, new List<DatosTecla> {
        new DatosTecla("FLECHA ARRIBA", false, true, false),
        new DatosTecla("F4", true, false, false),
        new DatosTecla("F4", true, false, true),
        new DatosTecla("FLECHA ARRIBA", true, true, false),
        new DatosTecla("FLECHA ARRIBA", false, true, true),
    }},
{
    25088, new List<DatosTecla> {
        new DatosTecla("RE PAG", false, true, false),
        new DatosTecla("F5", true, false, false),
        new DatosTecla("F5", true, false, true),
        new DatosTecla("RE PAG", true, true, false),
        new DatosTecla("RE PAG", false, true, true),
    }},
{
    25344, new List<DatosTecla> {
        new DatosTecla("F6", true, false, false),
        new DatosTecla("F6", true, false, true),
    }},
{
    25600, new List<DatosTecla> {
        new DatosTecla("FLECHA IZQUIERDA", false, true, false),
        new DatosTecla("F7", true, false, false),
        new DatosTecla("F7", true, false, true),
        new DatosTecla("FLECHA IZQUIERDA", true, true, false),
        new DatosTecla("FLECHA IZQUIERDA", false, true, true),
    }},
{
    25856, new List<DatosTecla> {
        new DatosTecla("F8", true, false, false),
        new DatosTecla("F8", true, false, true),
    }},
{
    26112, new List<DatosTecla> {
        new DatosTecla("FLECHA DERECHA", false, true, false),
        new DatosTecla("F9", true, false, false),
        new DatosTecla("BARRA ESPACIADORA", false, false, true),
        new DatosTecla("F9", true, false, true),
        new DatosTecla("FLECHA DERECHA", true, true, false),
        new DatosTecla("FLECHA DERECHA", false, true, true),
    }},
{
    26368, new List<DatosTecla> {
        new DatosTecla("F10", true, false, false),
        new DatosTecla("F10", true, false, true),
    }},
{
    26624, new List<DatosTecla> {
        new DatosTecla("FIN", false, true, false),
        new DatosTecla("F1", false, false, true),
        new DatosTecla("FIN", true, true, false),
        new DatosTecla("FIN", false, true, true),
    }},
{
    26880, new List<DatosTecla> {
        new DatosTecla("FLECHA ABAJO", false, true, false),
        new DatosTecla("F2", false, false, true),
        new DatosTecla("FLECHA ABAJO", true, true, false),
        new DatosTecla("FLECHA ABAJO", false, true, true),
    }},
{
    27136, new List<DatosTecla> {
        new DatosTecla("AV PAG", false, true, false),
        new DatosTecla("INICIO", true, false, false),
        new DatosTecla("F3", false, false, true),
        new DatosTecla("INICIO", true, false, true),
        new DatosTecla("AV PAG", true, true, false),
        new DatosTecla("NUMERO 7", true, false, false),
        new DatosTecla("AV PAG", false, true, true),
    }},
{
    27392, new List<DatosTecla> {
        new DatosTecla("INSERT", false, true, false),
        new DatosTecla("FLECHA ARRIBA", true, false, false),
        new DatosTecla("F4", false, false, true),
        new DatosTecla("FLECHA ARRIBA", true, false, true),
        new DatosTecla("INSERT", true, true, false),
        new DatosTecla("NUMERO 8", true, false, false),
        new DatosTecla("INSERT", false, true, true),
    }},
{
    27648, new List<DatosTecla> {
        new DatosTecla("SUPR", false, true, false),
        new DatosTecla("RE PAG", true, false, false),
        new DatosTecla("F5", false, false, true),
        new DatosTecla("RE PAG", true, false, true),
        new DatosTecla("SUPR", true, true, false),
        new DatosTecla("NUMERO 9", true, false, false),
        new DatosTecla("SUPR", false, true, true),
    }},
{
    27904, new List<DatosTecla> {
        new DatosTecla("F6", false, false, true),
    }},
{
    28160, new List<DatosTecla> {
        new DatosTecla("FLECHA IZQUIERDA", true, false, false),
        new DatosTecla("F7", false, false, true),
        new DatosTecla("FLECHA IZQUIERDA", true, false, true),
        new DatosTecla("NUMERO 4", true, false, false),
    }},
{
    28416, new List<DatosTecla> {
        new DatosTecla("F8", false, false, true),
    }},
{
    28672, new List<DatosTecla> {
        new DatosTecla("F11", false, true, false),
        new DatosTecla("FLECHA DERECHA", true, false, false),
        new DatosTecla("F9", false, false, true),
        new DatosTecla("FLECHA DERECHA", true, false, true),
        new DatosTecla("F11", true, true, false),
        new DatosTecla("NUMERO 6", true, false, false),
        new DatosTecla("F11", false, true, true),
    }},
{
    28928, new List<DatosTecla> {
        new DatosTecla("F12", false, true, false),
        new DatosTecla("F10", false, false, true),
        new DatosTecla("F12", true, true, false),
    }},
{
    29184, new List<DatosTecla> {
        new DatosTecla("FIN", true, false, false),
        new DatosTecla("FIN", true, false, true),
        new DatosTecla("NUMERO 1", true, false, false),
    }},
{
    29440, new List<DatosTecla> {
        new DatosTecla("FLECHA ABAJO", true, false, false),
        new DatosTecla("FLECHA ABAJO", true, false, true),
        new DatosTecla("NUMERO 2", true, false, false),
    }},
{
    29696, new List<DatosTecla> {
        new DatosTecla("AV PAG", true, false, false),
        new DatosTecla("INICIO", false, false, true),
        new DatosTecla("AV PAG", true, false, true),
        new DatosTecla("NUMERO 3", true, false, false),
        new DatosTecla("NUMERO 7", false, false, true),
    }},
{
    29952, new List<DatosTecla> {
        new DatosTecla("INSERT", true, false, false),
        new DatosTecla("FLECHA ARRIBA", false, false, true),
        new DatosTecla("INSERT", true, false, true),
        new DatosTecla("NUMERO 0", true, false, false),
        new DatosTecla("Num 8", false, false, true),
    }},
{
    30208, new List<DatosTecla> {
        new DatosTecla("SUPR", true, false, false),
        new DatosTecla("RE PAG", false, false, true),
        new DatosTecla("TECLA DECIMAL", true, false, false),
        new DatosTecla("NUMERO 9", false, false, true),
    }},
{
    30720, new List<DatosTecla> {
        new DatosTecla("FLECHA IZQUIERDA", false, false, true),
        new DatosTecla("NUMERO 4", false, false, true),
    }},
{
    31232, new List<DatosTecla> {
        new DatosTecla("F11", true, false, false),
        new DatosTecla("FLECHA DERECHA", false, false, true),
        new DatosTecla("F11", true, false, true),
        new DatosTecla("NUMERO 6", false, false, true),
    }},
{
    31488, new List<DatosTecla> {
        new DatosTecla("F12", true, false, false),
        new DatosTecla("F12", true, false, true),
    }},
{
    31744, new List<DatosTecla> {
        new DatosTecla("FIN", false, false, true),
        new DatosTecla("Num 1", false, false, true),
    }},
{
    32000, new List<DatosTecla> {
        new DatosTecla("FLECHA ABAJO", false, false, true),
        new DatosTecla("NUMERO 2", false, false, true),
    }},
{
    32256, new List<DatosTecla> {
        new DatosTecla("AV PAG", false, false, true),
        new DatosTecla("Num 3", false, false, true),
    }},
{
    32512, new List<DatosTecla> {
        new DatosTecla("INSERT", false, false, true),
        new DatosTecla("NUMERO 0", false, false, true),
    }},
{
    32768, new List<DatosTecla> {
        new DatosTecla("SUPR", false, false, true),
        new DatosTecla("Num Del", false, false, true),
    }},
{
    33792, new List<DatosTecla> {
        new DatosTecla("F11", false, false, true),
    }},
{
    34048, new List<DatosTecla> {
        new DatosTecla("F12", false, false, true),
    }},
};
    }
}
