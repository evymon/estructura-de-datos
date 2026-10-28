using System;
using System.IO; //para poder guardar la partida en un archivo
using System.Threading; //para poder usar el Thread.Sleep y esperar un ratito

namespace JuegoDeDamas
{
    class Program
    {
        // =====================================================
        // VARIABLES DEL JUEGO
        // aqui estan todas las variables que usa todo el programa
        // =====================================================

        static char[,] tablero = new char[8, 8]; //es la matriz para el tablero como el ajedrez
        static char turno = 'n'; //siemore incian las fichas negras

        // para guardar la direccion
        static int e1, m1, e2, m2;

        static bool[,] resaltadas = new bool[8, 8]; //para saber que casillas se pintan de verde

        static int cursorE = 2, cursorM = 1; //aqui va a estar el cuadrito azul, empieza en una fichita negra

        static string archivoPartida = "partida.txt"; //el archivo donde se va a ir guardando la partida

        static int tiempoPorTurno = 30; //los segundos que tiene cada jugador para moverse
        static int inicioTurno = 0; //para saber a que hora empezo el turno
        static int filaReloj = 0; //en que renglon de la pantalla esta el reloj para irlo actualizando
        static bool seAcaboElTiempo = false; //se pone en true cuando ya no le dio tiempo al jugador


        // =====================================================
        //  INICIO DEL PROGRAMA
        // aqui empieza todo, sale el menu y se juega la partida
        // =====================================================

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; //este va a permitir mostrar bien para el tablero
            MostrarInstrucciones(); // instruccciones

            Console.WriteLine("1 = jugar una partida nueva");
            Console.WriteLine("2 = ver la ultima partida guardada");
            Console.Write("que quieres hacer?: ");
            string? opcion = Console.ReadLine();

            if (opcion == "2")//si escogio el 2 se le enseña la partida que se guardo
            {
                VerPartida();
            }
            else//si no pues a jugar
            {
                JugarPartida();
            }

            Console.WriteLine("Presiona ENTER para salir");
            Console.ReadLine();
        }
        static void JugarPartida()//aqui se juega toda la partida de principio a fin
        {
            LlenarTablero(); //para llenar el tablero con las fichitas
            File.WriteAllText(archivoPartida, "");//se deja el archivo vacio para empezar a guardar esta partida nueva
            DibujarTablero();//este va a mostrar el tablero para que podamos jugar

            Console.WriteLine("Aqui esta el tablero. enter para comenzar a jugar :)");
            Console.ReadLine();

            bool juegoTerminado = false; // para que indique si el juego termino

            while (!juegoTerminado) //este es para que indique sin aun se puede seguir jugando
            {
                if (TieneMovimientos(turno))//aqui primero se revisa si el usuario puede haer algun movimiento
                {
                    JugarTurno();
                    turno = Contrario(turno);
                }
                else
                {
                    DibujarTablero();//si no se puede mover ninguna ficha, nimodo valio churros

                    Console.WriteLine("las " + Nombre(turno) + " ya no pueden mover...");
                    Console.WriteLine("¡ganan las " + Nombre(Contrario(turno)) + "!");
                    GuardarTexto("GANAN " + Nombre(Contrario(turno)));//tambien se guarda quien gano

                    juegoTerminado = true;
                }
            }

            Console.Write("quieres volver a ver la partida? (s/n): ");
            string? verla = Console.ReadLine();
            if (verla == "s" || verla == "S")//si dijo que si se le pasa la repeticion
            {
                VerPartida();
            }
        }


        // =====================================================
        // PANTALLA
        // aqui esta todo lo que se ve, las reglas y el tablero
        // =====================================================

        static void MostrarInstrucciones() //aqui estan las reglas por si se me olvidan
        {
            Console.WriteLine("=== DAMAS INGLESAS ===");
            Console.WriteLine();
            Console.WriteLine("como jugar:");
            Console.WriteLine("- el juego es para dos, empezando por las fichas negras y luego las blancas");
            Console.WriteLine("- con las flechas mueves el cuadrito azul y con ENTER eliges tu ficha");
            Console.WriteLine("- luego se pintan de verde las casillas a donde puede ir tu ficha");
            Console.WriteLine("- llevas el cuadrito azul a una casilla verde y le das ENTER para moverla");
            Console.WriteLine("- para comer escoges la casilla verde que esta atras de la ficha del otro equipo");
            Console.WriteLine("- con ESC te regresas a escoger otra ficha");
            Console.WriteLine("- cada jugador tiene " + tiempoPorTurno + " segundos para moverse, si no pierde su turno");
            Console.WriteLine("- las fichas normales solo pueden avanzar en diagonal");
            Console.WriteLine("- para comer una ficha, se tiene que dar un saltito en diagonal");
            Console.WriteLine("- si puedes comer, es obligatorio");
            Console.WriteLine("- si despues de comer puedes seguir comiendo, tienes que seguir");
            Console.WriteLine("- si una ficha llega al otro lado, se convierte en una dama");
            Console.WriteLine("- la dama puede moverse hacia adelante y hacia atras");
            Console.WriteLine("- gana el jugador que deje al rival sin movimientos");
            Console.WriteLine("- la partida se guarda sola por si la quieres volver a ver");
            Console.WriteLine();
            Console.WriteLine("presiona ENTER para continuar :)");
            Console.ReadLine();
        }
        static void LlenarTablero() //para colocar las fichas en las posiciones iniciales
        {
            for (int e = 0; e < 8; e++)
            {
                for (int m = 0; m < 8; m++)
                {
                    tablero[e, m] = ' ';//este es para que primero esten vacias las casillas

                    if ((e + m) % 2 == 1)//y este solo se utiliza para las casillas de negro
                    {
                        if (e < 3)//arriba estaran las fichas negras
                        {
                            tablero[e, m] = 'n';
                        }
                        else if (e > 4)//y abajo estaran las fichas blancas
                        {
                            tablero[e, m] = 'b';
                        }
                    }
                }
            }
        }
        static void DibujarTablero() //para dibujar el tablero
        {
            Console.Clear();
            Console.WriteLine("     A  B  C  D  E  F  G  H"); //este va a imprimir las letras de las columnas
            for (int e = 0; e < 8; e++) // y este es un bucle para que se pueda recorrer las filas
            {
                Console.Write(" " + (8 - e) + "  "); // va a imprimir el numero de la fila
                for (int m = 0; m < 8; m++)
                {
                    if ((e + m) % 2 == 1)// color de la casilla
                    {
                        Console.BackgroundColor = ConsoleColor.Red;//rojo para que se vean bien las blancas y las negras
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                    }
                    if (resaltadas[e, m])//si la fichita puede ir para aca se pinta de verde
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                    if (e == cursorE && m == cursorM)//y aqui se pinta el cuadrito azul para saber a donde va
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }

                    char ficha = tablero[e, m];
                    if (ficha == ' ')//si no hay una ficha se va a quedar vacia
                    {
                        Console.Write("   ");
                    }
                    else
                    {
                        if (ficha == 'n' || ficha == 'N')
                        {
                            Console.ForegroundColor = ConsoleColor.Black;//para las fichitas negras
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;//para las fichitas blancas
                        }
                        if (ficha == 'n')//las negras van rellenas
                        {
                            Console.Write(" ● ");
                        }
                        else if (ficha == 'N')
                        {
                            Console.Write(" ◆ ");
                        }
                        else if (ficha == 'b')//y las blancas van huecas para que no se confundan
                        {
                            Console.Write(" ○ ");
                        }
                        else
                        {
                            Console.Write(" ◇ ");
                        }
                    }
                }
                Console.ResetColor();
                Console.WriteLine("  " + (8 - e));
            }
            Console.WriteLine("     A  B  C  D  E  F  G  H");
            Console.WriteLine();
        }


        // =====================================================
        // TURNOS
        // aqui se controla lo que pasa en el turno de cada jugador
        // =====================================================

        static void JugarTurno()//esta al pendiente de lo que pasa en el turno del usuario
        {
            bool turnoTerminado = false;
            string mensaje = "";
            int filaObligada = -1;
            int columnaObligada = -1; //el -1 es porque no hay ninguna ficha obligada
            inicioTurno = Environment.TickCount;//aqui empieza a correr el tiempo del jugador

            while (!turnoTerminado)
            {
                bool debeCapturar = HayCapturas(turno);// va a checar si hay alguna captura

                bool seMovio = LeerMovimiento(mensaje);//aqui el usuario escoge su ficha y para donde la quiere mover
                mensaje = "";//ya se mostro el mensaje entonces se borra
                if (seAcaboElTiempo)//si ya no le dio tiempo pierde su turno
                {
                    seAcaboElTiempo = false;//se regresa a false para el siguiente turno
                    GuardarTexto(Nombre(turno) + " se les acabo el tiempo");//tambien se guarda en la partida
                    DibujarTablero();
                    Console.WriteLine("se te acabo el tiempo :( pierdes tu turno");
                    Thread.Sleep(2000);//se espera 2 segunditos para que alcance a leer
                    turnoTerminado = true;
                    continue;
                }
                if (!seMovio)//si se equivoco de tecla que vuelva a escoger
                {
                    mensaje = "elige otra vez tu ficha";
                    continue;
                }

                char ficha = tablero[e1, m1];//guardar la ficha que se quiere mover

                int diferenciaE = e2 - e1;//calcula cuando vana  cambiar la fila y la columna
                int diferenciaM = m2 - m1;

                if (!EsDeJugador(ficha, turno))//checa que la ficha sea del jugador
                {
                    mensaje = "ahi no hay una ficha tuya";
                    continue;
                }

                if (filaObligada != -1 && //la fichita debe seguir comiendo
                    (e1 != filaObligada || m1 != columnaObligada))
                {
                    mensaje = "tienes que seguir comiendo con la misma ficha";
                    continue;
                }
                if (tablero[e2, m2] != ' ')//no se puede caer encima de otra ficha
                {
                    mensaje = "ahi ya hay una ficha";
                    continue;
                }
                if (Math.Abs(diferenciaE) == 1 &&//el movimiento normal es en diagonal un saltito
                    Math.Abs(diferenciaM) == 1)
                {
                    if (debeCapturar)
                    {
                        mensaje = "hay una captura disponible";
                        continue;
                    }
                    if (!PuedeIrEnEsaDireccion(ficha, diferenciaE))//checa si se puede mover en esa direccion
                    {
                        mensaje = "las fichas normales no pueden ir para atras.";
                        continue;
                    }
                    GuardarMovimiento();//se guarda antes de mover para que quede de donde a donde fue
                    MoverFicha();

                    turnoTerminado = true;
                }
                else if (Math.Abs(diferenciaE) == 2 && //para comer se avanzan dos casiññas en diagonal
                         Math.Abs(diferenciaM) == 2)
                {
                    int eMedio = e1 + diferenciaE / 2;//sirve para calcular una dicha en el medio y poder comerla
                    int mMedio = m1 + diferenciaM / 2;

                    // Comprueba que la ficha pueda moverse en esa direccion.
                    if (!PuedeIrEnEsaDireccion(ficha, diferenciaE / 2))
                    {
                        mensaje = "las fichas normales no pueden ir para atras";
                        continue;
                    }

                    // La ficha que esta en medio debe pertenecer al rival.
                    if (!EsDeJugador(tablero[eMedio, mMedio], Contrario(turno)))
                    {
                        mensaje = "solo puedes brincar una ficha del otro equipo";
                        continue;
                    }
                    tablero[eMedio, mMedio] = ' ';// bai a la fichita que fue capturada

                    GuardarMovimiento();//se guarda que se comio una fichita
                    bool seCoronoLaFicha = MoverFicha();//se mueve la fichita
                    cursorE = e2;//el cuadrito azul se va con la fichita por si le toca seguir comiendo
                    cursorM = m2;
                    if (!seCoronoLaFicha && PuedeCapturar(e2, m2))//si no se hizo dama puede comerse a otra
                    {
                        filaObligada = e2;
                        columnaObligada = m2;
                        inicioTurno = Environment.TickCount;//se le dan otros 30 segundos para que siga comiendo
                    }
                    else
                    {
                        turnoTerminado = true;
                    }
                }
                else
                {
                    mensaje = "ese movimiento no se puede";
                }
            }
        }


        // =====================================================
        // TECLADO Y TEMPORIZADOR
        // aqui se leen las teclas y se cuenta el tiempo de cada jugador
        // =====================================================

        static bool LeerMovimiento(string mensaje)//aqui se leen las teclas que va apretando el usuario
        {
            bool fichaElegida = false;
            while (!fichaElegida)//se repite hasta que le de ENTER en alguna ficha
            {
                DibujarTablero();
                if (mensaje != "")//va a mostrar si el moviniento anterior fue incorrecto o no
                {
                    Console.WriteLine(mensaje);
                }
                Console.WriteLine("turno de " + Nombre(turno));
                if (HayCapturas(turno))//para avisarle que tiene que comer
                {
                    Console.WriteLine("tienes que comer una ficha");
                }
                Console.WriteLine("mueve el cuadrito azul con las flechas y presiona ENTER para elegir tu ficha");
                MostrarReloj();//para que vea cuanto tiempo le queda

                if (!EsperarTecla())//si no apreto nada y se le acabo el tiempo
                {
                    seAcaboElTiempo = true;
                    return false;
                }
                ConsoleKey tecla = Console.ReadKey(true).Key;//agarra la tecla que apreto, el true es para que no se vea en la pantalla
                if (tecla == ConsoleKey.Enter)//con el enter ya escogio su fichita
                {
                    fichaElegida = true;
                    if (EsDeJugador(tablero[cursorE, cursorM], turno))//si es su ficha se pintan de verde a donde puede ir
                    {
                        MarcarDestinos(cursorE, cursorM);
                        if (!HayVerdes())//si no se pinto ninguna es que esa ficha no se puede mover
                        {
                            fichaElegida = false;//que escoja otra
                            if (HayCapturas(turno))
                            {
                                mensaje = "esa ficha no puede comer, escoge una que si pueda comer";
                            }
                            else
                            {
                                mensaje = "esa ficha no se puede mover, escoge otra";
                            }
                        }
                    }
                }
                else
                {
                    MoverCursor(tecla);//con las flechas se mueve el cuadrito azul
                }
            }

            e1 = cursorE;//la fichita que escogio es la que esta donde se quedo el cuadrito azul
            m1 = cursorM;
            if (!EsDeJugador(tablero[e1, m1], turno))//si no es su ficha se regresa y luego le sale el mensaje
            {
                e2 = e1;
                m2 = m1;
                return true;
            }

            bool destinoElegido = false;
            while (!destinoElegido)//se repite hasta que le de ENTER en una casilla verde
            {
                DibujarTablero();
                Console.WriteLine("turno de " + Nombre(turno));
                Console.WriteLine("lleva el cuadrito azul a una casilla verde y presiona ENTER");
                Console.WriteLine("ESC = elegir otra ficha");
                MostrarReloj();//aqui tambien se ensena el reloj

                if (!EsperarTecla())//si se le acabo el tiempo mientras pensaba para donde moverse
                {
                    QuitarResaltadas();
                    seAcaboElTiempo = true;
                    return false;
                }
                ConsoleKey tecla = Console.ReadKey(true).Key;
                if (tecla == ConsoleKey.Enter && resaltadas[cursorE, cursorM])//solo se puede en las verdes
                {
                    destinoElegido = true;
                }
                else if (tecla == ConsoleKey.Escape)//se arrepintio, que escoja otra ficha
                {
                    QuitarResaltadas();
                    cursorE = e1;//el cuadrito se regresa a su ficha
                    cursorM = m1;
                    return false;
                }
                else
                {
                    MoverCursor(tecla);
                }
            }
            QuitarResaltadas();//ya se escogio, se despintan las verdes

            e2 = cursorE;//la casilla a donde va es donde quedo el cuadrito azul
            m2 = cursorM;
            return true;
        }
        static void MoverCursor(ConsoleKey tecla)//mueve el cuadrito azul con las flechas sin salirse del tablero
        {
            if (tecla == ConsoleKey.UpArrow && cursorE > 0)
            {
                cursorE--;
            }
            else if (tecla == ConsoleKey.DownArrow && cursorE < 7)
            {
                cursorE++;
            }
            else if (tecla == ConsoleKey.LeftArrow && cursorM > 0)
            {
                cursorM--;
            }
            else if (tecla == ConsoleKey.RightArrow && cursorM < 7)
            {
                cursorM++;
            }
        }
        static int SegundosQueQuedan()//para saber cuantos segundos le quedan al jugador
        {
            int pasaron = (Environment.TickCount - inicioTurno) / 1000;//turnotiemop
            return tiempoPorTurno - pasaron;
        }
        static void MostrarReloj()//ensena el tiempo que le queda al jugador
        {
            filaReloj = Console.CursorTop;//se guarda en que renglon quedo para despues escribir encima
            Console.WriteLine("tiempo: " + SegundosQueQuedan() + " segundos  ");
        }
        static bool EsperarTecla()//espera a que apriete una tecla, si se acaba el tiempo regresa false
        {
            int segundosAntes = SegundosQueQuedan();
            while (!Console.KeyAvailable)//mientras no apriete ninguna tecla
            {
                int segundos = SegundosQueQuedan();
                if (segundos <= 0)//ya valio, se acabo el tiempo
                {
                    return false;
                }
                if (segundos != segundosAntes)//si ya paso un segundo se cambia el numero del reloj
                {
                    Console.SetCursorPosition(0, filaReloj);//nos vamos al renglon del reloj
                    Console.Write("tiempo: " + segundos + " segundos  ");//y se escribe encima el nuevo tiempo
                    segundosAntes = segundos;
                }
                Thread.Sleep(100);//se espera un ratito para no estar checando a cada rato
            }
            return true;//si apreto una tecla a tiempo
        }


        // =====================================================
        // MOVIMIENTOS
        // aqui se mueven las fichitas y se pintan a donde pueden ir
        // =====================================================

        static bool MoverFicha()//mueve la fichita y revisa si llego a la otra fila para convertirse en dama
        {
            char ficha = tablero[e1, m1];
            bool seHizoDama = false;
            if (ficha == 'n' && e2 == 7)//si una ficha negra llega a la ultima fila se convierte en dama
            {
                ficha = 'N';
                seHizoDama = true;
            }
            else if (ficha == 'b' && e2 == 0)//y si es blcna y llega a la ultima fila se convierte en dama
            {
                ficha = 'B';
                seHizoDama = true;
            }
            tablero[e2, m2] = ficha;//se pone la fichita en su nueva posicion
            tablero[e1, m1] = ' ';//se vacia la casilla de donde venia
            return seHizoDama;
        }
        static void MarcarDestinos(int e, int m)//para marcar a donde se puede mover la fichita
        {
            char ficha = tablero[e, m];//guardar la ficha
            bool debeCapturar = HayCapturas(turno);//se checa si tiene que comer
            for (int dE = -1; dE <= 1; dE += 2)//se revisan las diagonales
            {
                for (int dM = -1; dM <= 1; dM += 2)
                {
                    if (!PuedeIrEnEsaDireccion(ficha, dE))//si no puede ir para ese lado se lo salta
                    {
                        continue;
                    }
                    int eSalto = e + 2 * dE;//donde caeria si brinca
                    int mSalto = m + 2 * dM;
                    if (DentroDelTablero(eSalto, mSalto) &&//si hay una del otro equipo y atras esta libre se puede comer
                        EsDeJugador(tablero[e + dE, m + dM], Contrario(turno)) &&
                        tablero[eSalto, mSalto] == ' ')
                    {
                        resaltadas[eSalto, mSalto] = true;//se pinta donde va a caer
                    }
                    else if (!debeCapturar &&//si no tiene que comer puede avanzar normal
                             DentroDelTablero(e + dE, m + dM) &&
                             tablero[e + dE, m + dM] == ' ')
                    {
                        resaltadas[e + dE, m + dM] = true;//se pinta la casilla de al lado
                    }
                }
            }
        }
        static void QuitarResaltadas()//para despintar todas las casillas verdes
        {
            for (int e = 0; e < 8; e++)//recorrer fila
            {
                for (int m = 0; m < 8; m++)//recorrer columna
                {
                    resaltadas[e, m] = false;//se deja sin pintar
                }
            }
        }
        static bool HayVerdes()//para saber si quedo pintada aunque sea una casilla verde
        {
            for (int e = 0; e < 8; e++)//recorrer fila
            {
                for (int m = 0; m < 8; m++)//recorrer columna
                {
                    if (resaltadas[e, m])
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        // =====================================================
        // VALIDACIONES
        // aqui se checa si los movimientos si se pueden hacer
        // =====================================================

        static bool PuedeCapturar(int e, int m)//checa si una ficha piede comerse a otra
        {
            char ficha = tablero[e, m];//guardar la ficha
            char rival = Contrario(char.ToLower(ficha));
            for (int dE = -1; dE <= 1; dE += 2)// se revisa las diagonales
            {
                for (int dM = -1; dM <= 1; dM += 2)
                {
                    int eSalto = e + 2 * dE;// se calcula donde caeria la fichita despues de saltar
                    int mSalto = m + 2 * dM;
                    if (PuedeIrEnEsaDireccion(ficha, dE) &&//se comprueban bien las posiciones
                        DentroDelTablero(e + dE, m + dM) &&
                        DentroDelTablero(eSalto, mSalto) &&
                        EsDeJugador(tablero[e + dE, m + dM], rival) &&
                        tablero[eSalto, mSalto] == ' ')
                    {
                        return true;//si resulto significa q si se puede comer
                    }
                }
            }

            return false;
        }
        static bool HayCapturas(char jugador)//se revisa si se puede comer alguna ficha
        {
            for (int e = 0; e < 8; e++) //recorrer fila
            {
                for (int m = 0; m < 8; m++) //recorrercolumna
                {
                    if (EsDeJugador(tablero[e, m], jugador) &&//por si llega haber alguna ficha q se pueda capturar
                        PuedeCapturar(e, m))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        static bool TieneMovimientos(char jugador)// va a revisar si el usuario aun tiene movimientos disponibles
        {
            for (int e = 0; e < 8; e++) //recorrer gila
            {
                for (int m = 0; m < 8; m++)//recorrer columna
                {
                    char ficha = tablero[e, m];

                    if (EsDeJugador(ficha, jugador))
                    {
                        // si puede capturar, todavia puede jugar.
                        if (PuedeCapturar(e, m))
                        {
                            return true;
                        }
                        for (int dE = -1; dE <= 1; dE += 2)// si la ficha no tiene captura se tiene que revisar los movimientos
                        {
                            for (int dM = -1; dM <= 1; dM += 2)
                            {
                                int nuevaE = e + dE;//se calcula para donde se pdria mover
                                int nuevaM = m + dM;

                                if (PuedeIrEnEsaDireccion(ficha, dE) &&// se checa que la casilla seleccionada sieste y si se pueda ocupar
                                    DentroDelTablero(nuevaE, nuevaM) &&
                                    tablero[nuevaE, nuevaM] == ' ')
                                {
                                    return true;// si lo pasa si se puede mover a la casilla
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        static bool DentroDelTablero(int e, int m)//para comprobar que alguna posicion si este dentro del tablero
        {
            return e >= 0 && e < 8 &&
                   m >= 0 && m < 8;
        }
        // este es oara revisar si la ficha puede avanzar en la direccion indicada
        static bool PuedeIrEnEsaDireccion(char ficha, int diferenciaE)
        {
            // Una mayuscula significa que la ficha es dama.
            if (char.IsUpper(ficha))
            {
                return true;
            }
            if (ficha == 'n')//lasfichitas negras avanzan hpara abajo
            {
                return diferenciaE == 1;
            }
            return diferenciaE == -1;//mientras que las fichitas blancas hacia arriba
        }
        static bool EsDeJugador(char ficha, char jugador)//compropbar si una ficha le perteneca al usuario indicado
        {
            return char.ToLower(ficha) == jugador;// este es para permitir reconocer las damas
        }


        // =====================================================
        // GUARDAR Y VER LA PARTIDA
        // aqui se guarda cada jugada en el archivo y se puede volver a ver
        // =====================================================

        static void GuardarTexto(string linea)//agrega un renglon al final del archivo de la partida
        {
            File.AppendAllText(archivoPartida, linea + "\n");
        }
        static void GuardarMovimiento()//guarda la jugada asi: NEGRAS C3 D4
        {
            GuardarTexto(Nombre(turno) + " " + Casilla(e1, m1) + " " + Casilla(e2, m2));
        }
        static string Casilla(int e, int m)//pasa la fila y la columna a como se ve en el tablero, ejemplo: C3
        {
            char letra = (char)('A' + m);//la columna 0 es la A, la 1 es la B y asi
            int numero = 8 - e;//la fila 0 es la 8 porque el tablero esta al reves
            return "" + letra + numero;
        }
        static void VerPartida()//para volver a ver la partida jugada por jugada
        {
            if (!File.Exists(archivoPartida))//si nunca se ha jugado no hay nada que ensenar
            {
                Console.WriteLine("todavia no hay ninguna partida guardada");
                return;
            }

            string[] jugadas = File.ReadAllLines(archivoPartida);//se leen todos los renglones del archivo
            LlenarTablero();//se empieza con el tablero como al principio
            QuitarResaltadas();
            cursorE = -1;//para que no salga el cuadrito azul en la repeticion
            cursorM = -1;

            DibujarTablero();
            Console.WriteLine("asi empezo la partida");
            Console.WriteLine("presiona ENTER para ver la siguiente jugada");
            Console.ReadLine();

            for (int i = 0; i < jugadas.Length; i++)//se va una por una
            {
                string[] partes = jugadas[i].Split(' ');//se separa, ejemplo: NEGRAS, C3, D4
                if (partes.Length == 3 && partes[1].Length == 2 && partes[2].Length == 2)//si es un movimiento
                {
                    string origen = partes[1];
                    string destino = partes[2];
                    m1 = origen[0] - 'A';//igual que antes, se convierte la letra en columna
                    e1 = 8 - (origen[1] - '0');//y el numero en fila
                    m2 = destino[0] - 'A';
                    e2 = 8 - (destino[1] - '0');

                    if (Math.Abs(e2 - e1) == 2)//si brinco dos casillas es que se comio una
                    {
                        tablero[(e1 + e2) / 2, (m1 + m2) / 2] = ' ';//se quita la que estaba en medio
                    }
                    MoverFicha();
                }

                DibujarTablero();
                Console.WriteLine("jugada " + (i + 1) + " de " + jugadas.Length + ": " + jugadas[i]);
                Console.WriteLine("presiona ENTER para ver la siguiente");
                Console.ReadLine();
            }
            Console.WriteLine("ahi se acabo la partida :)");
        }


        // =====================================================
        // AYUDANTITOS
        // funciones chiquitas que se usan en todos lados
        // =====================================================

        static char Contrario(char jugador) //poder cambiar, fichas negras por blancas y viceversa
        {
            if (jugador == 'n')
            {
                return 'b';
            }
            return 'n';
        }
        static string Nombre(char jugador)
        {
            if (jugador == 'n')
            {
                return "NEGRAS";
            }
            return "BLANCAS";
        }
    }
}
