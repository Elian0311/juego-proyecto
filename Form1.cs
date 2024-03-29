using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.Logging;
using System.Windows.Forms;

namespace juego
{
    public partial class Form1 : Form
    {
        //  imágenes de los botones del juego.
        Image imagen = Image.FromFile(@"C:\Users\elisa\OneDrive\Desktop\tarea 2 base de datos\juego\juego\imagenes\boton1.png");
        Image imagen2 = Image.FromFile(@"C:\Users\elisa\OneDrive\Desktop\tarea 2 base de datos\juego\juego\imagenes\boton2.png");
        //  matriz que representa el tablero del juego.
        int[,] matriz = new int[6, 7];
        // Variables para controlar las filas y columnas del tablero.
        string Fila = "", columna = "";
        // verifica si el juego ha terminado.
        bool juegoTerminado = false;
        // Pila para registrar el historial de movimientos.
        Stack<string> historialMovimientos = new Stack<string>();
        // Cola para controlar el orden de los turnos de los jugadores.
        Queue<int> ordenTurnos = new Queue<int>();
        // Variables para llevar la puntuación de cada jugador.
        int puntosJugador1 = 0;
        int puntosJugador2 = 0;





        public Form1()
        {
            InitializeComponent();
            InicializarTurnos();
            ActualizarMarcador();

        }




        // Método para inicializar los turnos de los jugadores.

        private void InicializarTurnos()
        {
            ordenTurnos.Enqueue(1);// Encola el turno del Jugador 1.
            ordenTurnos.Enqueue(2);// Encola el turno del Jugador 2.
        }





        // Método para registrar los movimientos de los jugadores en la matriz.
        private void LlenarMatriz()
        {
            if (juegoTerminado)
            {
                MessageBox.Show("El juego ha terminado. No se pueden hacer más movimientos.");
                return;
            }

            int col = int.Parse(columna);// Obtiene la columna seleccionada por el jugador.

            if (ColumnaLlena(col))
            {
                MessageBox.Show("La columna está llena. Por favor, elige otra columna.");
                return;
            }

            for (int i = 5; i >= 0; i--)
            {
                if (matriz[i, col] != 1 && matriz[i, col] != 2)
                {
                    historialMovimientos.Push($"[{i},{col}] {(ordenTurnos.Peek() == 1 ? "Jugador 1" : "Jugador 2")}");



                    // Turno del Jugador 1.
                    if (ordenTurnos.Peek() == 1)
                    {
                        matriz[i, col] = 1;
                        SeleccionarBoton(i, col);// Método para actualizar la interfaz gráfica.
                        if (VerificarVictoria(1))
                        {
                            MessageBox.Show("¡Jugador 1 ha ganado!");
                            puntosJugador1++; // Incrementar la puntuación del Jugador 1
                            juegoTerminado = true;
                        }
                        else
                        {
                            MessageBox.Show("Turno del Jugador 2");
                        }
                    }



                    // Turno del Jugador 2.
                    else
                    {
                        matriz[i, col] = 2;
                        SeleccionarBoton(i, col);
                        if (VerificarVictoria(2))
                        {
                            MessageBox.Show("¡Jugador 2 ha ganado!");
                            puntosJugador2++; 
                            juegoTerminado = true;
                        }
                        else if (JuegoTerminado())
                        {
                            MessageBox.Show("El juego ha terminado en empate.");
                            juegoTerminado = true;
                        }
                        else
                        {
                            MessageBox.Show("Turno del Jugador 1");
                        }
                    }

                    ordenTurnos.Enqueue(ordenTurnos.Dequeue());// Cambia el turno al siguiente jugador.
                    ActualizarMarcador(); // Actualizar el marcador después de cada turno
                    break;
                }
            }
        }



        // Método para verificar si una columna está llena.
        private bool ColumnaLlena(int col)
        {
            return matriz[0, col] != 0;
        }

    


        // Método para verificar si hay un ganador
        private bool VerificarVictoria(int jugador)
        {
            // Verificar filas
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (matriz[i, j] == jugador && matriz[i, j + 1] == jugador && matriz[i, j + 2] == jugador && matriz[i, j + 3] == jugador)
                    {
                        return true;
                    }
                }
            }

            // Verificar columnas
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (matriz[j, i] == jugador && matriz[j + 1, i] == jugador && matriz[j + 2, i] == jugador && matriz[j + 3, i] == jugador)
                    {
                        return true;
                    }
                }
            }

            // Verificar diagonales ascendentes
            for (int i = 3; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (matriz[i, j] == jugador && matriz[i - 1, j + 1] == jugador && matriz[i - 2, j + 2] == jugador && matriz[i - 3, j + 3] == jugador)
                    {
                        return true;
                    }
                }
            }

            // Verificar diagonales descendentes
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (matriz[i, j] == jugador && matriz[i + 1, j + 1] == jugador && matriz[i + 2, j + 2] == jugador && matriz[i + 3, j + 3] == jugador)
                    {
                        return true;
                    }
                }
            }

            // Si no se encontró ninguna combinación ganadora, devolver false
            return false;
        }




        // Método para verificar si todas las casillas del tablero están ocupadas.
        private bool JuegoTerminado()
        {
            // Verificar si todas las casillas están llenas
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (matriz[i, j] == 0)
                    {
                        return false; // Si hay al menos una casilla vacía, el juego no ha terminado.
                    }
                }
            }
            return true; // Si no hay casillas vacías, el juego ha terminado.
        }


       


    

        //_____________________________________________________________________________//
        //                     botones                              
        private void button43_Click(object sender, EventArgs e)//historial_____________//
        {
            MostrarHistorial();
        }

        private void ActualizarMarcador()
        {
            label3.Text = $"Jugador 1: {puntosJugador1} - Jugador 2: {puntosJugador2}";
        }

        private void button43_Click_1(object sender, EventArgs e)//cola_______________//
        {
            MostrarCola();
        }

        private void label3_Click(object sender, EventArgs e)//texto-marcador_________//
        {
            ActualizarMarcador();

        }

        private void reiniciar_Click(object sender, EventArgs e)//reiniciar juego____//
        {
            juegoTerminado = false;
            // 1. Reiniciar la matriz para vaciar el tablero.
            matriz = new int[6, 7];

            // 2. Limpiar los botones para eliminar las imágenes del juego anterior.
            LimpiarBotones(this); // 'this' representa el formulario principal.

            // 3. Limpiar el historial de movimientos y la cola de turnos.
            historialMovimientos.Clear();
            ordenTurnos.Clear();
            puntosJugador1 = 0;
            puntosJugador2 = 0;

            // 5. Actualizar el texto del marcador en tu interfaz de usuario
            label3.Text = $"Jugador 1: {puntosJugador1} - Jugador 2: {puntosJugador2}";

            // 6. Mostrar un mensaje indicando que se ha reiniciado el juego.
            MessageBox.Show("Juego reiniciado. Comienza un nuevo juego.");

            // Inicializar los turnos para comenzar con el Jugador 1
            InicializarTurnos();

            // 4. Mostrar un mensaje indicando que se ha reiniciado el juego.
            MessageBox.Show("Juego reiniciado. Comienza un nuevo juego.");
            // Inicializar los turnos para comenzar con el Jugador 1
            InicializarTurnos();
        }

        private void button44_Click_1(object sender, EventArgs e)
        {
            // Verifica si el juego ha terminado
            if (juegoTerminado)
            {
                // Si el juego ha terminado, inicia un nuevo juego
                ReiniciarPartida();
            }
            else
            {
                // Si el juego no ha terminado, muestra un mensaje indicando que no se puede iniciar un nuevo juego
                MessageBox.Show("No se puede iniciar una nueva partida hasta que el juego actual termine.");
            }
        }
        //______________________________________________________________________________I


        private void ReiniciarPartida()
        {
            // Reinicia la matriz para vaciar el tablero
            matriz = new int[6, 7];

            // Limpia los botones para eliminar las imágenes del juego anterior
            LimpiarBotones(this); // 'this' representa el formulario principal.

            // Restablece todas las imágenes de los botones a null para eliminar cualquier imagen existente
            foreach (Control control in this.Controls)
            {
                if (control is Button)
                {
                    ((Button)control).Image = null;
                }
            }

            // Mensaje indicando que se ha reiniciado el juego
            MessageBox.Show("Nueva partida iniciada. Comienza un nuevo juego.");

            // Reinicializa la variable juegoTerminado a false para indicar que el juego está en curso
            juegoTerminado = false;

            // Reinicializa los turnos para comenzar con el Jugador 1
            InicializarTurnos();
        }

        private void MostrarHistorial()
        {
            string movimientos = "Historial de movimientos:\n";// Inicializar la cadena para almacenar los movimientos
            foreach (string movimiento in historialMovimientos)
            {
                movimientos += $"{movimiento}\n"; // Agregar cada movimiento al historial
            }
            MessageBox.Show(movimientos);// Mostrar el historial en un cuadro de mensaje
        }

        private void MostrarCola()
        {
            if (ordenTurnos.Count > 0) // Verificar si hay jugadores en la cola de turnos
            {
                int jugadorActual = ordenTurnos.Peek(); // Obtener el jugador actual sin sacarlo de la cola
                MessageBox.Show($"Turno del Jugador {jugadorActual}"); // Mostrar el turno del jugador actual en un cuadro de mensaje
            }

        }

        

        private void LimpiarBotones(Control control)
        {
         
            foreach (Control subControl in control.Controls)
            {
                // Verificar si el subControl es un botón
                if (subControl is Button)
                {
                    // Limpiar la imagen del botón
                    ((Button)subControl).Image = null;
                }

                // Si el subControl tiene más controles dentro, llamar recursivamente a LimpiarBotones
                if (subControl.HasChildren)
                {
                    LimpiarBotones(subControl);
                }
            }
        }

        private void SeleccionarBoton(int fila, int col)
        {
            // Concatenar la fila y la columna para obtener el código del botón seleccionado
            string codigoboton = fila.ToString() + col.ToString();

            // Utilizar un switch para determinar qué acción tomar según el código del botón seleccionado
            switch (codigoboton)
            {
                case "00":
                    // Verificar el turno del jugador
                    if (ordenTurnos.Peek() == 1)
                    {
                        // Si es el turno del Jugador 1, establecer la imagen del botón como la imagen del Jugador 1
                        button1.Image = imagen;
                        // Cambiar el turno al Jugador 2

                    }
                    else
                    {
                        // Si es el turno del Jugador 2, establecer la imagen del botón como la imagen del Jugador 2
                        button1.Image = imagen2;
                        // Cambiar el turno al Jugador 1

                    }


                    break;

                //patrón similar al caso anterior
                case "10":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button8.Image = imagen;

                    }
                    else
                    {
                        button8.Image = imagen2;

                    }
                    break;
                case "20":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button15.Image = imagen;

                    }
                    else
                    {
                        button15.Image = imagen2;

                    }
                    break;
                case "30":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button22.Image = imagen;

                    }
                    else
                    {
                        button22.Image = imagen2;

                    }
                    break;
                case "40":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button29.Image = imagen;

                    }
                    else
                    {
                        button29.Image = imagen2;

                    }
                    break;
                case "50":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button36.Image = imagen;

                    }
                    else
                    {
                        button36.Image = imagen2;

                    }
                    break;

                case "01":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button2.Image = imagen;

                    }
                    else
                    {
                        button2.Image = imagen2;

                    }
                    break;
                case "11":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button9.Image = imagen;

                    }
                    else
                    {
                        button9.Image = imagen2;

                    }
                    break;
                case "21":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button16.Image = imagen;

                    }
                    else
                    {
                        button16.Image = imagen2;

                    }
                    break;
                case "31":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button23.Image = imagen;

                    }
                    else
                    {
                        button23.Image = imagen2;

                    }
                    break;
                case "41":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button30.Image = imagen;

                    }
                    else
                    {
                        button30.Image = imagen2;

                    }
                    break;

                case "51":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button37.Image = imagen;

                    }
                    else
                    {
                        button37.Image = imagen2;

                    }
                    break;


                case "02":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button3.Image = imagen;

                    }
                    else
                    {
                        button3.Image = imagen2;

                    }
                    break;
                case "12":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button10.Image = imagen;

                    }
                    else
                    {
                        button10.Image = imagen2;

                    }
                    break;
                case "22":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button17.Image = imagen;

                    }
                    else
                    {
                        button17.Image = imagen2;

                    }
                    break;
                case "32":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button24.Image = imagen;

                    }
                    else
                    {
                        button24.Image = imagen2;

                    }
                    break;
                case "42":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button31.Image = imagen;

                    }
                    else
                    {
                        button31.Image = imagen2;

                    }
                    break;
                case "52":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button38.Image = imagen;

                    }
                    else
                    {
                        button38.Image = imagen2;

                    }
                    break;

                case "03":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button4.Image = imagen;

                    }
                    else
                    {
                        button4.Image = imagen2;

                    }
                    break;
                case "13":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button11.Image = imagen;

                    }
                    else
                    {
                        button11.Image = imagen2;

                    }
                    break;
                case "23":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button18.Image = imagen;

                    }
                    else
                    {
                        button18.Image = imagen2;

                    }
                    break;
                case "33":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button25.Image = imagen;

                    }
                    else
                    {
                        button25.Image = imagen2;

                    }
                    break;

                case "43":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button32.Image = imagen;

                    }
                    else
                    {
                        button32.Image = imagen2;

                    }
                    break;

                case "53":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button39.Image = imagen;

                    }
                    else
                    {
                        button39.Image = imagen2;

                    }
                    break;

                case "04":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button5.Image = imagen;

                    }
                    else
                    {
                        button5.Image = imagen2;

                    }
                    break;
                case "14":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button12.Image = imagen;

                    }
                    else
                    {
                        button12.Image = imagen2;

                    }
                    break;
                case "24":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button19.Image = imagen;

                    }
                    else
                    {
                        button19.Image = imagen2;

                    }
                    break;
                case "34":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button26.Image = imagen;

                    }
                    else
                    {
                        button26.Image = imagen2;

                    }
                    break;
                case "44":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button33.Image = imagen;

                    }
                    else
                    {
                        button33.Image = imagen2;

                    }
                    break;
                case "54":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button40.Image = imagen;

                    }
                    else
                    {
                        button40.Image = imagen2;

                    }
                    break;

                case "05":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button6.Image = imagen;

                    }
                    else
                    {
                        button6.Image = imagen2;

                    }
                    break;
                case "15":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button13.Image = imagen;

                    }
                    else
                    {
                        button13.Image = imagen2;

                    }
                    break;
                case "25":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button20.Image = imagen;
                    }
                    else
                    {
                        button20.Image = imagen2;
                    }
                    break;
                case "35":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button27.Image = imagen;
                    }
                    else
                    {
                        button27.Image = imagen2;
                    }
                    break;
                case "45":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button34.Image = imagen;
                    }
                    else
                    {
                        button34.Image = imagen2;
                    }
                    break;
                case "55":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button41.Image = imagen;
                    }
                    else
                    {
                        button41.Image = imagen2;
                    }
                    break;

                case "06":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button7.Image = imagen;
                    }
                    else
                    {
                        button7.Image = imagen2;
                    }
                    break;
                case "16":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button14.Image = imagen;
                    }
                    else
                    {
                        button14.Image = imagen2;
                    }
                    break;
                case "26":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button21.Image = imagen;
                    }
                    else
                    {
                        button21.Image = imagen2;
                    }
                    break;
                case "36":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button28.Image = imagen;
                    }
                    else
                    {
                        button28.Image = imagen2;
                    }
                    break;
                case "46":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button35.Image = imagen;
                    }
                    else
                    {
                        button35.Image = imagen2;
                    }
                    break;
                case "56":
                    if (ordenTurnos.Peek() == 1)
                    {
                        button42.Image = imagen;
                        ;
                    }
                    else
                    {
                        button42.Image = imagen2;

                    }
                    break;

            }

        }

        //__________________________________________________________________________________________________________________________________________
        private void button1_Click(object sender, EventArgs e)
        {
            // Al hacer clic en el botón en la fila 0, columna 0, establecer la fila y columna correspondientes
            Fila = "0"; columna = "0";
            // Llamar al método LlenarMatriz para realizar las acciones correspondientes
            LlenarMatriz();

        }


        private void button8_Click(object sender, EventArgs e)
        {
            Fila = "1"; columna = "0";
            LlenarMatriz();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Fila = "2"; columna = "0";
            LlenarMatriz();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Fila = "3"; columna = "0";
            LlenarMatriz();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            Fila = "4"; columna = "0";
            LlenarMatriz();
        }

        private void button36_Click(object sender, EventArgs e)
        {
            Fila = "5"; columna = "0";
            LlenarMatriz();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Fila = "0"; columna = "1";
            LlenarMatriz();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Fila = "1"; columna = "1";
            LlenarMatriz();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Fila = "2"; columna = "1";
            LlenarMatriz();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Fila = "3"; columna = "1";
            LlenarMatriz();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            Fila = "4"; columna = "1";
            LlenarMatriz();
        }

        private void button37_Click(object sender, EventArgs e)
        {
            Fila = "5"; columna = "1";
            LlenarMatriz();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Fila = "0"; columna = "2";
            LlenarMatriz();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Fila = "1"; columna = "2";
            LlenarMatriz();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Fila = "2"; columna = "2";
            LlenarMatriz();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Fila = "3"; columna = "2";
            LlenarMatriz();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            Fila = "4"; columna = "2";
            LlenarMatriz();
        }

        private void button38_Click(object sender, EventArgs e)
        {
            Fila = "5"; columna = "1";
            LlenarMatriz();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            Fila = "0"; columna = "3";
            LlenarMatriz();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Fila = "1"; columna = "3";
            LlenarMatriz();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Fila = "2"; columna = "3";
            LlenarMatriz();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            Fila = "3"; columna = "3";
            LlenarMatriz();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            Fila = "4"; columna = "3";
            LlenarMatriz();
        }

        private void button39_Click(object sender, EventArgs e)
        {
            Fila = "5"; columna = "3";
            LlenarMatriz();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Fila = "0"; columna = "4";
            LlenarMatriz();
        }
        private void button12_Click(object sender, EventArgs e)
        {
            Fila = "1"; columna = "4";
            LlenarMatriz();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Fila = "2"; columna = "4";
            LlenarMatriz();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            Fila = "3"; columna = "4";
            LlenarMatriz();
        }

        private void button33_Click(object sender, EventArgs e)
        {
            Fila = "4"; columna = "4";
            LlenarMatriz();
        }

        private void button40_Click(object sender, EventArgs e)
        {
            Fila = "5"; columna = "4";
            LlenarMatriz();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Fila = "0"; columna = "5";
            LlenarMatriz();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Fila = "1"; columna = "5";
            LlenarMatriz();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Fila = "2"; columna = "5";
            LlenarMatriz();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            Fila = "3"; columna = "5";
            LlenarMatriz();
        }

        private void button34_Click(object sender, EventArgs e)
        {
            Fila = "4"; columna = "5";
            LlenarMatriz();
        }

        private void button41_Click(object sender, EventArgs e)
        {
            Fila = "5"; columna = "5";
            LlenarMatriz();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Fila = "0"; columna = "6";
            LlenarMatriz();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Fila = "1"; columna = "6";
            LlenarMatriz();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Fila = "2"; columna = "6";
            LlenarMatriz();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            Fila = "3"; columna = "6";
            LlenarMatriz();
        }

        private void button35_Click(object sender, EventArgs e)
        {
            Fila = "4"; columna = "6";
            LlenarMatriz();
        }

        private void button42_Click(object sender, EventArgs e)
        {
            Fila = "5"; columna = "6";
            LlenarMatriz();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        // guardar 
        private void button46_Click(object sender, EventArgs e)
        {

        }
        // cargar
        private void button45_Click(object sender, EventArgs e)
        {

        }

    
    }
}