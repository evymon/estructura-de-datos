using System; 

namespace JuegoDeDamas
{
    class Program
    {
        static char[,] tablero = new char[8, 8]; //es la matriz para el tablero como el ajedrez
        static char turno = 'n'; //siemore incian las fichas negras

        // pa guardar la direccion 
        static int e1, m1, e2, m2;

        static bool[,] resaltadas = new bool[8, 8]; //casillas a donde se puede mover la ficha elegida
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; //este va a permitir mostrar bien para el tablero
            MostrarInstrucciones(); // instruccciones
            LlenarTablero(); //pa llenar el tablero con las fichitas
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

                    juegoTerminado = true;
                }
            }
            Console.WriteLine("Presiona ENTER para salir");
            Console.ReadLine();
        }
        static void MostrarInstrucciones() //aqui estan las reglas por si se me olvidan 
        {
            Console.WriteLine("=== DAMAS INGLESAS ===");
            Console.WriteLine();
            Console.WriteLine("como jugar:");
            Console.WriteLine("- el juego es para dos, empezando por las fichas negras y luego las blancas");
            Console.WriteLine("- se va a mover escribiendo la casilla de origen y la de destino. ejemplo: C3 D4");
            Console.WriteLine("- las fichas normales solo pueden avanzar en diagonal");
            Console.WriteLine("- para comer una ficha, se tiene que dar un saltito en diagonal");
            Console.WriteLine("- si puedes comer, es obligatorio");
            Console.WriteLine("- si después de comer puedes seguir comiendo, tienes que seguir");
            Console.WriteLine("- si una ficha llega al otro lado, se convierte en una dama");
            Console.WriteLine("- la dama puede moverse hacia adelante y hacia atrás");
            Console.WriteLine("- gana el jugador que deje al rival sin movimientos");
            Console.WriteLine();
            Console.WriteLine("presiona ENTER para que veas la tablita :)");
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
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                    }
                    if (resaltadas[e, m])//si es una casilla a donde se puede avanzar se pinta de verde
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
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
                        if (char.IsUpper(ficha))//las mayusculas son las damas
                        {
                            Console.Write(" \u2666 ");
                        }
                        else
                        {
                            Console.Write(" \u25CF ");//minuscula vienen siendo las fichas normales
                        }
                    }
                }
                Console.ResetColor();
                Console.WriteLine("  " + (8 - e));
            }
            Console.WriteLine("     A  B  C  D  E  F  G  H");
            Console.WriteLine();
        }
        static void JugarTurno()//esta al pendiente de lo que pasa en el turno del usuario
        {
            bool turnoTerminado = false;
            string mensaje = "";
            int filaObligada = -1;
            int columnaObligada = -1; //el -1 es porque no hay ninguna ficha obligada

            while (!turnoTerminado)
            {
                DibujarTablero();
                if (mensaje != "")//va a mostrar si el moviniento anterior fue incorrecto o no
                {
                    Console.WriteLine(mensaje);
                    mensaje = "";
                }

                bool debeCapturar = HayCapturas(turno);// va a checar si hay alguna captura 

                Console.WriteLine("turno de " + Nombre(turno));

                if (debeCapturar)
                {
                    Console.WriteLine("tienes que comer una ficha");
                }
                Console.Write("escribe la ficha que quieres mover (ejemplo: C3): ");
                if (!LeerMovimiento())//leey revisa lo que escribio el usuaio
                {
                    mensaje = "eso no se entiende, escríbelo como C3 D4";
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
                if (Math.Abs(diferenciaE) == 1 && Math.Abs(diferenciaM) == 1 &&//si escribio la casilla donde esta la ficha rival
                    EsDeJugador(tablero[e2, m2], Contrario(turno)))
                {
                    int eAtras = e2 + diferenciaE;//la casilla que esta detras de la ficha rival
                    int mAtras = m2 + diferenciaM;
                    if (DentroDelTablero(eAtras, mAtras) && tablero[eAtras, mAtras] == ' ')
                    {
                        e2 = eAtras;//se cambia el destino para que brinque y se la coma
                        m2 = mAtras;
                        diferenciaE = diferenciaE * 2;
                        diferenciaM = diferenciaM * 2;
                    }
                }
                if (tablero[e2, m2] != ' ')//para asegurar que donde queremos ir esta libre
                {
                    mensaje = "la casilla ya está ocupada";
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
                        mensaje = "las fichas normales no pueden ir para atrás.";
                        continue;
                    }
                    MoverFicha();

                    turnoTerminado = true;
                }
                else if (Math.Abs(diferenciaE) == 2 && //para comer se avanzan dos casiññas en diagonal
                         Math.Abs(diferenciaM) == 2)
                {
                    int eMedio = e1 + diferenciaE / 2;//sirve para calcular una dicha en el medio y poder comerla
                    int mMedio = m1 + diferenciaM / 2;

                    // Comprueba que la ficha pueda moverse en esa dirección.
                    if (!PuedeIrEnEsaDireccion(ficha, diferenciaE / 2))
                    {
                        mensaje = "las fichas normales no pueden ir para atrás";
                        continue;
                    }

                    // La ficha que está en medio debe pertenecer al rival.
                    if (!EsDeJugador(tablero[eMedio, mMedio], Contrario(turno)))
                    {
                        mensaje = "solo puedes brincar una ficha del otro equipo";
                        continue;
                    }
                    tablero[eMedio, mMedio] = ' ';// bai a la fichita que fue capturada

                    bool seCoronoLaFicha = MoverFicha();//se mueve la fichita
                    if (!seCoronoLaFicha && PuedeCapturar(e2, m2))//si no se hizo dama puede comerse a otra
                    {
                        filaObligada = e2;
                        columnaObligada = m2;
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
        static bool LeerMovimiento()//lee el movimiento del usuario
        {
            string? texto = Console.ReadLine();

            if (texto == null)
            {
                return false;
            }
            texto = texto.Replace(" ", "").ToUpper();//quita espacios y puede apsar a mayusculas
            if (texto.Length == 2)//si solo escribio la ficha, se le muestra a donde puede avanzar
            {
                m1 = texto[0] - 'A';
                e1 = 8 - (texto[1] - '0');
                if (!DentroDelTablero(e1, m1))
                {
                    return false;
                }
                if (!EsDeJugador(tablero[e1, m1], turno))//si no es su ficha, luego le sale el mensaje
                {
                    e2 = e1;
                    m2 = m1;
                    return true;
                }
                MarcarDestinos(e1, m1);
                DibujarTablero();//se dibuja el tablero con las casillas resaltadas
                Array.Clear(resaltadas, 0, resaltadas.Length);//se quitan para el siguiente dibujo

                Console.WriteLine("turno de " + Nombre(turno));
                Console.Write("¿a dónde la quieres mover? (casillas verdes): ");
                string? destino = Console.ReadLine();
                if (destino == null)
                {
                    return false;
                }
                texto = texto + destino.Replace(" ", "").ToUpper();//se junta para que quede como C3D4
            }
            if (texto.Length != 4)//despues de limpiar debe tener 4 caracteres
            {
                return false;
            }

            m1 = texto[0] - 'A';//ayuda a convertir las letras de las columnas 
            m2 = texto[2] - 'A';
            e1 = 8 - (texto[1] - '0');//igual pero se convierte los numeros para q se puedan adaptar al tablero
            e2 = 8 - (texto[3] - '0');

            return DentroDelTablero(e1, m1) &&//para confirmar que si este la posicion en el tablero
                   DentroDelTablero(e2, m2);
        }
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
        static void MarcarDestinos(int e, int m)//marca las casillas a donde puede avanzar la ficha
        {
            char ficha = tablero[e, m];
            bool debeCapturar = HayCapturas(turno);
            for (int dE = -1; dE <= 1; dE += 2)//se revisan las diagonales
            {
                for (int dM = -1; dM <= 1; dM += 2)
                {
                    if (!PuedeIrEnEsaDireccion(ficha, dE))
                    {
                        continue;
                    }
                    int eSalto = e + 2 * dE;
                    int mSalto = m + 2 * dM;
                    if (DentroDelTablero(eSalto, mSalto) &&//si puede comer se marca la casilla donde caeria
                        EsDeJugador(tablero[e + dE, m + dM], Contrario(turno)) &&
                        tablero[eSalto, mSalto] == ' ')
                    {
                        resaltadas[eSalto, mSalto] = true;
                    }
                    else if (!debeCapturar &&//si no hay que comer, se marca la casilla de al lado
                             DentroDelTablero(e + dE, m + dM) &&
                             tablero[e + dE, m + dM] == ' ')
                    {
                        resaltadas[e + dE, m + dM] = true;
                    }
                }
            }
        }
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
                        // si puede capturar, todavía puede jugar.
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
        // este es oara revisar si la ficha puede avanzar en la dirección indicada
        static bool PuedeIrEnEsaDireccion(char ficha, int diferenciaE)
        {
            // Una mayúscula significa que la ficha es dama.
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