namespace FlyingChessGame
{
    class PlayFlyingChess
    {
        static int[] gameMap = new int[100];
        static int[] gamePlayerPos = new int[2];
        static string[] playerName = { "玩家一", "玩家二" };
        static int index;
        static void Main(string[] args)
        {
            GameTitle();

            Console.WriteLine(playerName[0]);
            Console.WriteLine(playerName[1]);

            Console.WriteLine("游戏玩法：");

            Console.WriteLine("输入任意键开始游戏");


            Console.Clear();

            InitGameMap();
            System.Console.WriteLine(gameMap);
            DrawMap();

            Update();

            if (gamePlayerPos[0] > gamePlayerPos[1])
            {
                Console.WriteLine("{0}获得胜利", playerName[0]);
            }
            else
            {
                Console.WriteLine("{0}获得胜利", playerName[1]);
            }

            Console.ReadKey();
        }



        private static void Update()
        {
            index = 0;

            while (gamePlayerPos[0] < 99 && gamePlayerPos[1] < 99)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("{0}开始投掷色子", playerName[index % 2]);
                Console.ReadKey(true);
                int ran = GetRanden(1, 7);
                System.Console.WriteLine("{0}投掷出{1}点", playerName[index % 2], ran);
                gamePlayerPos[index % 2] += ran;

                Console.ReadKey(true);
                Console.WriteLine("{0}开始行动", playerName[index % 2]);

                if (gamePlayerPos[0] == gamePlayerPos[1] && gamePlayerPos[0] != 0)
                {
                    Console.WriteLine("{0}走到了{1}的背后进行攻击,{2}后退6格", playerName[index % 2], playerName[(index + 1) % 2], playerName[(index + 1) % 2]);
                    if ((gamePlayerPos[1] - 6) < 0)
                    {
                        gamePlayerPos[1] = 0;
                    }
                    else
                    {
                        gamePlayerPos[1] -= 6;

                    }
                    Console.ReadKey(true);
                }
                else//Map Components
                {
                    switch (gameMap[gamePlayerPos[0]])
                    {
                        case 0:
                            Console.WriteLine("{0}踩到方块，安全", playerName[index % 2]);
                            Console.ReadKey(true);
                            break;

                        case 1:
                            Console.WriteLine("{0}获得道具,可以进行一次选择 a--交换位置 b--让对方退6个格子", playerName[index % 2]);
                            string input = Console.ReadLine();

                            while (true)
                            {
                                if (input == "a")
                                {
                                    Console.WriteLine("玩家{0}选择跟玩家{1}交换位置", playerName[index % 2], playerName[(index + 1) % 2]);
                                    int temp = gamePlayerPos[0];
                                    gamePlayerPos[0] = gamePlayerPos[1];

                                    gamePlayerPos[1] = temp;
                                    Console.WriteLine("交换完毕，请按任意键进行游戏");
                                    Console.ReadKey(true);
                                    break;
                                }
                                else if (input == "b")
                                {
                                    Console.WriteLine("玩家{0}选择令玩家{1}后退六个格子", playerName[index % 2], playerName[(index + 1) % 2]);
                                    if ((gamePlayerPos[1] - 6) < 0)
                                    {
                                        gamePlayerPos[1] = 0;
                                    }
                                    else
                                    {
                                        gamePlayerPos[1] -= 6;
                                    }
                                    Console.WriteLine("玩家{0}本回合得退回{1}位置,按任意键进行游戏", gamePlayerPos[(index + 1) % 2], 6);
                                    Console.ReadKey(true);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("输入无效字符,请重新输入");
                                    input = Console.ReadLine();
                                    
                                }
                            }
                            break;

                        case 2:
                            Console.WriteLine("{0}后退，后退6格", gamePlayerPos[index % 2]);

                            if ((gamePlayerPos[index % 2] - 6) < 0)
                            {
                                gamePlayerPos[index % 2] = 0;
                            }
                            else
                            {
                                gamePlayerPos[index % 2] -= 6;
                            }
                            break;
                        case 3:
                            Console.WriteLine("{0}踩到炸弹，让对方暂停一回合", gamePlayerPos[index % 2]);
                            index++;
                            Console.ReadKey(true);
                            break;

                        case 4:
                            Console.WriteLine("{0}进入隧道，将前进10格", playerName[index % 2]);
                            int[] tunnel = { 20, 25, 45, 63, 72, 88, 90 };
                            gamePlayerPos[index % 2] += 10;
                            Console.WriteLine("{0}从{1}隧道口出来", playerName[index % 2], gamePlayerPos[index % 2]);
                            Console.ReadKey(true);
                            break;

                    }
                    index++;
                    Console.Clear();
                    DrawMap();
                }


            }

        }

        private static int GetRanden(int v1, int v2)
        {

            Random random = new Random();
            return random.Next(v1, v2);
        }

        private static void GameTitle()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("飞行棋双人小游戏");
            Console.WriteLine("************************************");
        }

        private static void InitGameMap()
        {
            //双圈
            int[] planeItem = { 6, 23, 40, 55, 69, 83 };
            for (int i = 0; i < planeItem.Length; i++)
            {
                gameMap[planeItem[i]] = 1;
            }
            //地雷
            int[] bombItem = { 5, 13, 17, 33, 38, 50, 64, 80, 94 };
            for (int i = 0; i < bombItem.Length; i++)
            {
                gameMap[bombItem[i]] = 2;
            }
            //暂停
            int[] pauseItem = { 9, 27, 60, 93 };
            for (int i = 0; i < pauseItem.Length; i++)
            {
                gameMap[pauseItem[i]] = 3;
            }
            //隧道
            int[] tunnelItem = { 20, 25, 45, 63, 88, 90 };
            for (int i = 0; i < tunnelItem.Length; i++)
            {
                gameMap[tunnelItem[i]] = 4;
            }

        }

        private static void DrawMap()
        {
            System.Console.WriteLine("玩家一用A表示，玩家二用B表示");
            System.Console.WriteLine("图例:□普通方块 ◇道具 ●炸弹 ＄飞机 ¤隧道");

            for (int i = 0; i < 30; i++)
            {
                Draw(i);
            }
            System.Console.WriteLine();
            for (int i = 30; i < 35; i++)
            {
                for (int j = 0; j <= 30; j++)
                {
                    Console.Write(" ");
                }
                Draw(i);
                System.Console.WriteLine();

            }
            for (int i = 64; i >= 35; i--)
            {
                Draw(i);
            }
            System.Console.WriteLine();
            for (int i = 65; i < 70; i++)
            {
                Draw(i);
                System.Console.WriteLine();

            }
            for (int i = 70; i < 100; i++)
            {
                Draw(i);
            }
            Console.WriteLine();


        }
        private static void Draw(int i)
        {
            if (gamePlayerPos[0] == gamePlayerPos[1] && gamePlayerPos[0] == i)
            {
                System.Console.Write("<>");
            }
            else if (gamePlayerPos[0] == i)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.Write("A");
            }
            else if (gamePlayerPos[1] == i)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("B");
            }
            else
            {
                switch (gameMap[i])
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("□");
                        break;

                    case 1:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("◇");
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("●");
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("＄");
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("¤");
                        break;
                }
            }
        }

    }
}