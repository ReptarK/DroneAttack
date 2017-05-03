using MyGame.GererPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using AtelierXNA;

namespace MyGame
{
    public static class PointsDePatrouille
    {
        public static List<Case> ListePoints;
        public static List<Case> ListeSpawns;
        static PointsDePatrouille()
        {
            ListePoints = new List<Case>();
            ListeSpawns = new List<Case>();

            if (Data.NomCarte == Data.CarteName.BridgeMap)
                GenererPointsBridge();
            else
                GenererPointsOuai();
        }

        public static void GenererPointsBridge()
        {
            List<Case> listeParents = new List<Case>();
            Case Case1 = new Case(new Vector3(63, 10, 22), null); ListePoints.Add(Case1);
            Case Case2 = new Case(new Vector3(66, 10, 122), null); ListePoints.Add(Case2);
            Case Case3 = new Case(new Vector3(26, 10, 172), null); ListePoints.Add(Case3);
            Case Case4 = new Case(new Vector3(24, 10, 284), null); ListePoints.Add(Case4);
            Case Case5 = new Case(new Vector3(23, 10, 418), null); ListePoints.Add(Case5);
            Case Case6 = new Case(new Vector3(85, 10, 251), null); ListePoints.Add(Case6);
            Case Case7 = new Case(new Vector3(85, 10, 352), null); ListePoints.Add(Case7);
            Case Case8 = new Case(new Vector3(74, 10, 470), null); ListePoints.Add(Case8);
            Case Case9 = new Case(new Vector3(166, 10, 143), null); ListePoints.Add(Case9);
            Case Case10 = new Case(new Vector3(194, 10, 272), null); ListePoints.Add(Case10);
            Case Case11 = new Case(new Vector3(243, 25, 22), null); ListePoints.Add(Case11);
            Case Case12 = new Case(new Vector3(242, 25, 122), null); ListePoints.Add(Case12);
            Case Case13 = new Case(new Vector3(242, 10, 238), null); ListePoints.Add(Case13);
            Case Case14 = new Case(new Vector3(251, 10, 352), null); ListePoints.Add(Case14);
            Case Case15 = new Case(new Vector3(248, 10, 472), null); ListePoints.Add(Case15);
            Case Case16 = new Case(new Vector3(351, 10, 153), null); ListePoints.Add(Case16);
            Case Case17 = new Case(new Vector3(335, 10, 273), null); ListePoints.Add(Case17);
            Case Case18 = new Case(new Vector3(340, 10, 400), null); ListePoints.Add(Case18);
            Case Case19 = new Case(new Vector3(340, 10, 470), null); ListePoints.Add(Case19);
            Case Case20 = new Case(new Vector3(398, 10, 22), null); ListePoints.Add(Case20);
            Case Case21 = new Case(new Vector3(409, 10, 101), null); ListePoints.Add(Case21);
            Case Case22 = new Case(new Vector3(413, 10, 161), null); ListePoints.Add(Case22);
            Case Case23 = new Case(new Vector3(374, 10, 215), null); ListePoints.Add(Case23);
            Case Case24 = new Case(new Vector3(400, 10, 331), null); ListePoints.Add(Case24);
            Case Case25 = new Case(new Vector3(472, 10, 24), null); ListePoints.Add(Case25);
            Case Case26 = new Case(new Vector3(468, 10, 100), null); ListePoints.Add(Case26);
            Case Case27 = new Case(new Vector3(469, 10, 180), null); ListePoints.Add(Case27);
            Case Case28 = new Case(new Vector3(464, 10, 275), null); ListePoints.Add(Case28);
            Case Case29 = new Case(new Vector3(460, 10, 375), null); ListePoints.Add(Case29);
            Case Case30 = new Case(new Vector3(460, 10, 471), null); ListePoints.Add(Case30);
            Case Case31 = new Case(new Vector3(14, 10, 21), null); ListePoints.Add(Case31);

            //Spawn
            Case Spawn1 = new Case(new Vector3(248, 100, 600), null); ListePoints.Add(Spawn1); ListeSpawns.Add(Spawn1);
            Case Spawn2 = new Case(new Vector3(-100, 100, 284), null); ListePoints.Add(Spawn2); ListeSpawns.Add(Spawn2);
            Case Spawn3 = new Case(new Vector3(550, 150, 275), null); ListePoints.Add(Spawn3); ListeSpawns.Add(Spawn3);
            //Case Case31 = new Case(new Vector3(108, 10, 87), null); ListePoints.Add(Case31);
            //Case Case32 = new Case(new Vector3(110, 10, 146), null); ListePoints.Add(Case32);
            //Case Case33 = new Case(new Vector3(200, 10, 84), null); ListePoints.Add(Case33);
            //Case Case34 = new Case(new Vector3(228, 10, 131), null); ListePoints.Add(Case34);
            //Case Case35 = new Case(new Vector3(145, 10, 330), null); ListePoints.Add(Case35);
            //Case Case36 = new Case(new Vector3(156, 10, 385), null); ListePoints.Add(Case36);
            //Case Case37 = new Case(new Vector3(140, 10, 450), null); ListePoints.Add(Case37);
            //Case Case38 = new Case(new Vector3(180, 10, 476), null); ListePoints.Add(Case38);
            //Case Case39 = new Case(new Vector3(210, 10, 337), null); ListePoints.Add(Case39);
            //Case Case40 = new Case(new Vector3(145, 10, 115), null); ListePoints.Add(Case40);
            //Case Case41 = new Case(new Vector3(182, 10, 83), null); ListePoints.Add(Case41);
            //Case Case42 = new Case(new Vector3(110, 10, 146), null); ListePoints.Add(Case42);
            //Case Case43 = new Case(new Vector3(110, 10, 146), null); ListePoints.Add(Case43);
            //Case Case44 = new Case(new Vector3(110, 10, 146), null); ListePoints.Add(Case44);
            //Case Case45 = new Case(new Vector3(110, 10, 146), null); ListePoints.Add(Case45);
            //Case Case46 = new Case(new Vector3(110, 10, 146), null); ListePoints.Add(Case46);
            //Case Case47 = new Case(new Vector3(110, 10, 146), null); ListePoints.Add(Case47);
            //Case Case48 = new Case(new Vector3(110, 10, 146), null); ListePoints.Add(Case48);

            listeParents.Add(Case15);
            Spawn1.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case4);
            Spawn2.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case28);
            Spawn3.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case2);
            listeParents.Add(Case11);
            listeParents.Add(Case31);
            Case1.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case3);
            listeParents.Add(Case6);
            listeParents.Add(Case1);
            listeParents.Add(Case9);
            //listeParents.Add(Case31);
            //listeParents.Add(Case32);
            //listeParents.Add(Case33);
            Case2.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case4);
            listeParents.Add(Case2);
            //listeParents.Add(Case32);
            //listeParents.Add(Case33);
            Case3.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case5);
            listeParents.Add(Case3);
            Case4.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case8);
            listeParents.Add(Case4);
            Case5.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case2);
            listeParents.Add(Case7);
            //listeParents.Add(Case33);
            //listeParents.Add(Case34);
            Case6.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case6);
            listeParents.Add(Case14);
            //listeParents.Add(Case35);
            //listeParents.Add(Case36);
            Case7.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case5);
            listeParents.Add(Case15);
            //listeParents.Add(Case37);
            Case8.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case2);
            listeParents.Add(Case12);
            //listeParents.Add(Case40);
            //listeParents.Add(Case41);
            Case9.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case6);
            listeParents.Add(Case13);
            listeParents.Add(Case17);
            //listeParents.Add(Case34);
            Case10.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case1);
            listeParents.Add(Case12);
            listeParents.Add(Case20);
            Case11.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case9);
            listeParents.Add(Case13);
            listeParents.Add(Case16);
            listeParents.Add(Case11);
            Case12.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case10);
            listeParents.Add(Case12);
            Case13.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case7);
            listeParents.Add(Case15);
            listeParents.Add(Case18);
            //listeParents.Add(Case39);
            Case14.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case8);
            listeParents.Add(Case14);
            //listeParents.Add(Case38);
            Case15.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case12);
            listeParents.Add(Case22);
            Case16.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case10);
            listeParents.Add(Case18);
            listeParents.Add(Case24);
            listeParents.Add(Case23);
            Case17.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case19);
            listeParents.Add(Case17);
            listeParents.Add(Case14);
            Case18.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case18);
            listeParents.Add(Case30);
            Case19.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case11);
            listeParents.Add(Case21);
            listeParents.Add(Case25);
            listeParents.Add(Case26);
            Case20.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case22);
            listeParents.Add(Case20);
            listeParents.Add(Case25);
            listeParents.Add(Case26);
            listeParents.Add(Case27);
            Case21.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case16);
            listeParents.Add(Case23);
            listeParents.Add(Case21);
            listeParents.Add(Case26);
            listeParents.Add(Case27);
            Case22.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case17);
            listeParents.Add(Case22);
            listeParents.Add(Case24);
            Case23.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case17);
            listeParents.Add(Case29);
            listeParents.Add(Case28);
            Case24.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case20);
            listeParents.Add(Case21);
            listeParents.Add(Case26);
            Case25.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case20);
            listeParents.Add(Case21);
            listeParents.Add(Case22);
            listeParents.Add(Case25);
            listeParents.Add(Case27);
            Case26.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case22);
            listeParents.Add(Case28);
            listeParents.Add(Case26);
            Case27.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case24);
            listeParents.Add(Case29);
            listeParents.Add(Case27);
            Case28.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case24);
            listeParents.Add(Case28);
            listeParents.Add(Case30);
            Case29.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case19);
            listeParents.Add(Case29);
            Case30.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case2);
            Case31.ListeParents = listeParents;
            listeParents = new List<Case>();

            //listeParents.Add(Case2);
            //listeParents.Add(Case31);
            //listeParents.Add(Case32);
            //listeParents.Add(Case40);
            //Case31.ListeParents = listeParents;
            //listeParents = new List<Case>();

            //listeParents.Add(Case2);
            //listeParents.Add(Case31);
            //listeParents.Add(Case33);
            //listeParents.Add(Case40);
            //Case32.ListeParents = listeParents;
            //listeParents = new List<Case>();

            //listeParents.Add(Case2);
            //listeParents.Add(Case6);
            //listeParents.Add(Case34);
            //Case33.ListeParents = listeParents;
            //listeParents = new List<Case>();


            //listeParents.Add(Case6);
            //listeParents.Add(Case10);
            //listeParents.Add(Case33);
            //Case34.ListeParents = listeParents;
            //listeParents = new List<Case>();


            //listeParents.Add(Case7);
            //listeParents.Add(Case39);
            //listeParents.Add(Case36);
            //Case35.ListeParents = listeParents;
            //listeParents = new List<Case>();


            //listeParents.Add(Case7);
            //listeParents.Add(Case35);
            //listeParents.Add(Case39);
            //Case36.ListeParents = listeParents;
            //listeParents = new List<Case>();


            //listeParents.Add(Case8);
            //listeParents.Add(Case38);
            //Case37.ListeParents = listeParents;
            //listeParents = new List<Case>();


            //listeParents.Add(Case37);
            //listeParents.Add(Case15);
            //Case38.ListeParents = listeParents;
            //listeParents = new List<Case>();


            //listeParents.Add(Case14);
            //listeParents.Add(Case15);
            //listeParents.Add(Case36);
            //listeParents.Add(Case35);
            //Case39.ListeParents = listeParents;
            //listeParents = new List<Case>();

            //listeParents.Add(Case9);
            //listeParents.Add(Case32);
            //listeParents.Add(Case31);
            //listeParents.Add(Case32);
            //Case40.ListeParents = listeParents;
            //listeParents = new List<Case>();

            //listeParents.Add(Case9);
            //listeParents.Add(Case40);
            //listeParents.Add(Case12);
            //listeParents.Add(Case32);
            //Case41.ListeParents = listeParents;
            //listeParents = new List<Case>();
        }

        public static void GenererPointsOuai()
        {
            List<Case> listeParents = new List<Case>();

            Case Case1 = new Case(new Vector3(21, 10, 17), null); ListePoints.Add(Case1);
            Case Case2 = new Case(new Vector3(14, 10, 95), null); ListePoints.Add(Case2);
            Case Case3 = new Case(new Vector3(17, 10, 210), null); ListePoints.Add(Case3);
            Case Case4 = new Case(new Vector3(20, 10, 315), null); ListePoints.Add(Case4);
            Case Case5 = new Case(new Vector3(20, 10, 418), null); ListePoints.Add(Case5);
            Case Case6 = new Case(new Vector3(82, 10, 20), null); ListePoints.Add(Case6);
            Case Case7 = new Case(new Vector3(73, 10, 80), null); ListePoints.Add(Case7);
            Case Case8 = new Case(new Vector3(70, 10, 210), null); ListePoints.Add(Case8);
            Case Case9 = new Case(new Vector3(100, 10, 270), null); ListePoints.Add(Case9);
            Case Case10 = new Case(new Vector3(90, 10, 340), null); ListePoints.Add(Case10);
            Case Case11 = new Case(new Vector3(79, 25, 433), null); ListePoints.Add(Case11);
            Case Case12 = new Case(new Vector3(80, 25, 484), null); ListePoints.Add(Case12);
            Case Case13 = new Case(new Vector3(121, 10, 454), null); ListePoints.Add(Case13);
            Case Case14 = new Case(new Vector3(126, 15, 375), null); ListePoints.Add(Case14);
            Case Case15 = new Case(new Vector3(160, 10, 165), null); ListePoints.Add(Case15);
            Case Case16 = new Case(new Vector3(177, 10, 277), null); ListePoints.Add(Case16);
            Case Case17 = new Case(new Vector3(176, 15, 325), null); ListePoints.Add(Case17);
            Case Case18 = new Case(new Vector3(177, 20, 375), null); ListePoints.Add(Case18);
            Case Case19 = new Case(new Vector3(177, 15, 426), null); ListePoints.Add(Case19);
            Case Case20 = new Case(new Vector3(176, 10, 477), null); ListePoints.Add(Case20);
            Case Case21 = new Case(new Vector3(200, 10, 212), null); ListePoints.Add(Case21);
            Case Case22 = new Case(new Vector3(230, 10, 436), null); ListePoints.Add(Case22);
            Case Case23 = new Case(new Vector3(230, 10, 170), null); ListePoints.Add(Case23);
            Case Case24 = new Case(new Vector3(237, 10, 313), null); ListePoints.Add(Case24);
            Case Case25 = new Case(new Vector3(275, 10, 260), null); ListePoints.Add(Case25);
            Case Case26 = new Case(new Vector3(275, 10, 485), null); ListePoints.Add(Case26);
            Case Case27 = new Case(new Vector3(290, 10, 365), null); ListePoints.Add(Case27);
            Case Case28 = new Case(new Vector3(328, 10, 320), null); ListePoints.Add(Case28);
            Case Case29 = new Case(new Vector3(331, 10, 425), null); ListePoints.Add(Case29);
            Case Case30 = new Case(new Vector3(356, 10, 474), null); ListePoints.Add(Case30);
            Case Case31 = new Case(new Vector3(400, 20, 472), null); ListePoints.Add(Case31);
            Case Case32 = new Case(new Vector3(403, 20, 378), null); ListePoints.Add(Case32);
            Case Case33 = new Case(new Vector3(400, 20, 280), null); ListePoints.Add(Case33);
            Case Case34 = new Case(new Vector3(474, 60, 284), null); ListePoints.Add(Case34);
            Case Case35 = new Case(new Vector3(474, 60, 378), null); ListePoints.Add(Case35);
            Case Case36 = new Case(new Vector3(474, 60, 472), null); ListePoints.Add(Case36);
            Case Case37 = new Case(new Vector3(428, 10, 228), null); ListePoints.Add(Case37);
            Case Case38 = new Case(new Vector3(375, 10, 221), null); ListePoints.Add(Case38);
            Case Case39 = new Case(new Vector3(383, 10, 141), null); ListePoints.Add(Case39);
            Case Case40 = new Case(new Vector3(476, 10, 180), null); ListePoints.Add(Case40);
            Case Case41 = new Case(new Vector3(466, 10, 128), null); ListePoints.Add(Case41);
            Case Case42 = new Case(new Vector3(472, 20, 35), null); ListePoints.Add(Case42);
            Case Case43 = new Case(new Vector3(378, 10, 40), null); ListePoints.Add(Case43);
            Case Case44 = new Case(new Vector3(328, 10, 106), null); ListePoints.Add(Case44);
            Case Case45 = new Case(new Vector3(258, 10, 29), null); ListePoints.Add(Case45);
            Case Case46 = new Case(new Vector3(205, 10, 35), null); ListePoints.Add(Case46);
            Case Case47 = new Case(new Vector3(175, 10, 102), null); ListePoints.Add(Case47);
            Case Case48 = new Case(new Vector3(122, 10, 85), null); ListePoints.Add(Case48);
            Case Case49 = new Case(new Vector3(296, 10, 160), null); ListePoints.Add(Case49);
            Case Case50 = new Case(new Vector3(325, 10, 248), null); ListePoints.Add(Case50);
            Case Case51 = new Case(new Vector3(345, 10, 183), null); ListePoints.Add(Case51);
            Case Case52 = new Case(new Vector3(225, 20, 373), null); ListePoints.Add(Case52);
            Case Case53 = new Case(new Vector3(450, 20, 480), null); ListePoints.Add(Case53);
            Case Case54 = new Case(new Vector3(450, 20, 386), null); ListePoints.Add(Case54);
            Case Case55 = new Case(new Vector3(450, 20, 318), null); ListePoints.Add(Case55);
            Case Case56 = new Case(new Vector3(480, 15, 260), null); ListePoints.Add(Case56);
            //Spawn
            Case Spawn1 = new Case(new Vector3(248, 150, -100), null); ListePoints.Add(Spawn1); ListeSpawns.Add(Spawn1);
            Case Spawn2 = new Case(new Vector3(-100, 100, 284), null); ListePoints.Add(Spawn2); ListeSpawns.Add(Spawn2);
            Case Spawn3 = new Case(new Vector3(550, 150, 275), null); ListePoints.Add(Spawn3); ListeSpawns.Add(Spawn3);


            listeParents.Add(Case45);
            Spawn1.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case4);
            Spawn2.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case36);
            Spawn3.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case2);
            listeParents.Add(Case6);
            Case1.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case3);
            listeParents.Add(Case1);
            listeParents.Add(Case7);
            listeParents.Add(Case8);
            Case2.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case4);
            listeParents.Add(Case2);
            listeParents.Add(Case8);
            Case3.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case5);
            listeParents.Add(Case3);
            Case4.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case11);
            listeParents.Add(Case4);
            Case5.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case1);
            listeParents.Add(Case7);
            listeParents.Add(Case48);
            Case6.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case6);
            listeParents.Add(Case8);
            listeParents.Add(Case48);
            Case7.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case3);
            listeParents.Add(Case7);
            listeParents.Add(Case9);
            Case8.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case8);
            listeParents.Add(Case10);
            listeParents.Add(Case16);
            listeParents.Add(Case14);
            Case9.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case9);
            listeParents.Add(Case11);
            listeParents.Add(Case14);
            Case10.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case10);
            listeParents.Add(Case12);
            listeParents.Add(Case13);
            listeParents.Add(Case14);
            Case11.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case11);
            listeParents.Add(Case13);
            Case12.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case11);
            listeParents.Add(Case12);
            listeParents.Add(Case19);
            listeParents.Add(Case20);
            Case13.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case10);
            listeParents.Add(Case11);
            listeParents.Add(Case18);
            Case14.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case21);
            listeParents.Add(Case23);
            Case15.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case17);
            listeParents.Add(Case21);
            listeParents.Add(Case24);
            Case16.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case16);
            listeParents.Add(Case18);
            listeParents.Add(Case24);
            Case17.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case14);
            listeParents.Add(Case17);
            listeParents.Add(Case19);
            listeParents.Add(Case52);
            Case18.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case18);
            listeParents.Add(Case20);
            listeParents.Add(Case22);
            Case19.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case13);
            listeParents.Add(Case29);
            listeParents.Add(Case22);
            listeParents.Add(Case26);
            Case20.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case15);
            listeParents.Add(Case16);
            listeParents.Add(Case23);
            listeParents.Add(Case25);
            Case21.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case19);
            listeParents.Add(Case20);
            listeParents.Add(Case52);
            listeParents.Add(Case26);
            listeParents.Add(Case27);
            Case22.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case15);
            listeParents.Add(Case21);
            listeParents.Add(Case49);
            Case23.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case16);
            listeParents.Add(Case17);
            listeParents.Add(Case25);
            listeParents.Add(Case27);
            listeParents.Add(Case52);
            Case24.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case24);
            listeParents.Add(Case52);
            listeParents.Add(Case21);
            Case25.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case22);
            listeParents.Add(Case30);
            Case26.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case24);
            listeParents.Add(Case28);
            listeParents.Add(Case29);
            listeParents.Add(Case52);
            Case27.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case24);
            listeParents.Add(Case27);
            listeParents.Add(Case29);
            Case28.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case27);
            listeParents.Add(Case28);
            listeParents.Add(Case30);
            Case29.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case26);
            listeParents.Add(Case29);
            listeParents.Add(Case31);
            Case30.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case30);
            listeParents.Add(Case32);
            listeParents.Add(Case34);
            listeParents.Add(Case35);
            Case31.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case31);
            listeParents.Add(Case33);
            listeParents.Add(Case34);
            listeParents.Add(Case36);
            Case32.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case32);
            listeParents.Add(Case35);
            listeParents.Add(Case36);
            listeParents.Add(Case37);
            listeParents.Add(Case38);
            Case33.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case31);
            listeParents.Add(Case32);
            listeParents.Add(Case35);
            Case34.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case31);
            listeParents.Add(Case32);
            listeParents.Add(Case33);
            listeParents.Add(Case34);
            listeParents.Add(Case36);
            Case35.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case32);
            listeParents.Add(Case35);
            listeParents.Add(Case36);
            Case36.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case38);
            listeParents.Add(Case33);
            listeParents.Add(Case40);
            Case37.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case33);
            listeParents.Add(Case37);
            listeParents.Add(Case50);
            listeParents.Add(Case51);
            Case38.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case38);
            listeParents.Add(Case41);
            listeParents.Add(Case43);
            listeParents.Add(Case44);
            listeParents.Add(Case51);
            Case39.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case36);
            listeParents.Add(Case37);
            listeParents.Add(Case41);
            Case40.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case40);
            listeParents.Add(Case42);
            listeParents.Add(Case39);
            listeParents.Add(Case43);
            Case41.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case41);
            listeParents.Add(Case43);
            listeParents.Add(Case39);
            Case42.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case39);
            listeParents.Add(Case42);
            listeParents.Add(Case41);
            listeParents.Add(Case44);
            Case43.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case39);
            listeParents.Add(Case43);
            listeParents.Add(Case45);
            Case44.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case46);
            listeParents.Add(Case44);
            Case45.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case45);
            listeParents.Add(Case47);
            Case46.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case46);
            listeParents.Add(Case48);
            Case47.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case6);
            listeParents.Add(Case7);
            listeParents.Add(Case47);
            Case48.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case23);
            listeParents.Add(Case50);
            listeParents.Add(Case51);
            Case49.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case25);
            listeParents.Add(Case38);
            listeParents.Add(Case49);
            listeParents.Add(Case51);
            Case50.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case49);
            listeParents.Add(Case50);
            listeParents.Add(Case39);
            listeParents.Add(Case38);
            Case51.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case18);
            listeParents.Add(Case22);
            listeParents.Add(Case24);
            listeParents.Add(Case27);
            Case52.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case34);
            listeParents.Add(Case31);
            listeParents.Add(Case32);
            Case53.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case31);
            listeParents.Add(Case32);
            listeParents.Add(Case34);
            listeParents.Add(Case53);
            listeParents.Add(Case35);
            listeParents.Add(Case55);
            Case54.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case32);
            listeParents.Add(Case33);
            listeParents.Add(Case36);
            listeParents.Add(Case54);
            listeParents.Add(Case35);
            Case55.ListeParents = listeParents;
            listeParents = new List<Case>();

            listeParents.Add(Case40);
            listeParents.Add(Case37);
            listeParents.Add(Case36);
            Case56.ListeParents = listeParents;
            listeParents = new List<Case>();
        }
    }
}
