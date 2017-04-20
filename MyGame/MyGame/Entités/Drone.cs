using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MyGame.Entités;
using Microsoft.Xna.Framework.Graphics;
using MyGame;
using MyGame.GererPath;
using Microsoft.Xna.Framework.Audio;

namespace AtelierXNA
{
    //Ennemy
    public class Drone : ObjetDeDémo, IDestructible
    {
        RessourcesManager<SoundEffect> GestionnaireDeSons;
        SoundEffect DroneSound;
        SoundEffect BulletMiss;
        SoundEffect BulletHit;

        Case StartCase;

        LocalPlayer MyPlayer;

        float Compteur { get; set; }

        public int Health;

        Caméra1stPerson CaméraJeu;

        Random GenerateurRandom;


        BoundingBox boiteDeCollision;
        public BoundingBox BoiteDeCollision
        {
            get { return UpdateBoiteCollision(this.Modèle, this.GetMonde()); }
        }

        BoundingSphere sphereDeTir;
        public BoundingSphere SphereDeTir
        {
            get { return UpdateSphereDeTir(); }
        }

        public bool ADétruire { get; set; }

        BoundingSphere SphèreDuDrone { get; set; }

        protected BoundingBox UpdateBoiteCollision(Model model, Matrix worldTransform)
        {
            // Initialize minimum and maximum corners of the bounding box to max and min values
            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            // For each mesh of the model
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {
                    // Vertex buffer parameters
                    int vertexStride = meshPart.VertexBuffer.VertexDeclaration.VertexStride;
                    int vertexBufferSize = meshPart.NumVertices * vertexStride;

                    // Get vertex data as float
                    float[] vertexData = new float[vertexBufferSize / sizeof(float)];
                    meshPart.VertexBuffer.GetData<float>(vertexData);

                    // Iterate through vertices (possibly) growing bounding box, all calculations are done in world space
                    for (int i = 0; i < vertexBufferSize / sizeof(float); i += vertexStride / sizeof(float))
                    {
                        Vector3 transformedPosition = Vector3.Transform(new Vector3(vertexData[i], vertexData[i + 1], vertexData[i + 2]), worldTransform);

                        min = Vector3.Min(min, transformedPosition);
                        max = Vector3.Max(max, transformedPosition);
                    }
                }
            }

            // Create and return bounding box
            return new BoundingBox(min, max);
        }

        const float RADIUS_TIR = 100;
        protected BoundingSphere UpdateSphereDeTir()
        {
            sphereDeTir.Center = Position;

            return sphereDeTir;
        }
        public Drone(Game jeu, string nomModèle, float échelleInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, float intervalleMAJ, Color couleur, Case startCase, float divisionDeplacement, int health) 
            : base(jeu, nomModèle, échelleInitiale, rotationInitiale, positionInitiale, intervalleMAJ, couleur)
        {
            StartCase = startCase;
            ActualCase = StartCase;
            Health = health;
            ADétruire = false;
            Position = startCase.Position;
            DivisionDeplacement = divisionDeplacement;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            MyPlayer = Game.Services.GetService(typeof(LocalPlayer)) as LocalPlayer;
            GestionnaireDeSons = new RessourcesManager<SoundEffect>(Game, "Sounds");
            GenerateurRandom = Game.Services.GetService(typeof(Random)) as Random;
        }

        protected override void EffectuerMiseÀJour()
        {
            Compteur += 0.05f;
            Vector3 hauteur = new Vector3(0, (float)Math.Sin(Compteur), 0);
            Position += hauteur * 0.1f; //CHANGER AU BESOIN

            base.EffectuerMiseÀJour();
            base.CalculerMonde();
        }

        public override void Initialize()
        {
            base.Initialize();
            CaméraJeu = Game.Services.GetService(typeof(Caméra1stPerson)) as Caméra1stPerson;

            DroneSound = GestionnaireDeSons.Find("DroneSound");
            BulletHit = GestionnaireDeSons.Find("BulletHit");
            BulletMiss = GestionnaireDeSons.Find("BulletMiss");

            sphereDeTir = new BoundingSphere(Position, RADIUS_TIR);

            Compteur = (float)GenerateurRandom.NextDouble();

            GererPathFinding();
            SphèreDuDrone = Modèle.Meshes[0].BoundingSphere;
            for (int i = 1; i< Modèle.Meshes.Count;++i)
            {
                SphèreDuDrone = BoundingSphere.CreateMerged(SphèreDuDrone, Modèle.Meshes[i].BoundingSphere);
            }
        }

        float TempsÉcouléSound = 0;
        float TempsÉcouléDestruction = 0;
        float TempsÉcouléUpdatePath = 0;
        float TempsÉcouléDeplacement = 0;
        float TempsÉcouléRandom = 0;
        float TempsÉcouléTir = 0;
        Vector3 Deplacement;
        bool UpdatePath = true;
        Case ActualCase;
        int compteur = 0;
        float DivisionDeplacement;
        public override void Update(GameTime gameTime)
        {
            if(!MortPlayerMenu.ShowDeathRecap)
            {
                float RandomUpdate = 1f;

                if ((TempsÉcouléRandom += (float)gameTime.ElapsedGameTime.TotalSeconds) > 0.1f)
                    RandomUpdate = (float)GenerateurRandom.Next(80, 101) / 100f;

                base.Update(gameTime);

                if ((TempsÉcouléTir += (float)gameTime.ElapsedGameTime.TotalSeconds) > 0.5f * RandomUpdate)
                {
                    TempsÉcouléTir = 0;
                    GererTir();
                }

                if ((TempsÉcouléSound += (float)gameTime.ElapsedGameTime.TotalSeconds) > 0.5f * RandomUpdate)
                {
                    TempsÉcouléSound = 0;

                    GererSound();
                }

                if ((TempsÉcouléDestruction += (float)gameTime.ElapsedGameTime.TotalSeconds) > 0.5f)
                {
                    if (Health <= 0)
                        ADétruire = true;

                    TempsÉcouléDestruction = 0;
                }

                if (UpdatePath)
                {
                    GererPathFinding();

                    Deplacement = (CloseList[1].Position - ActualCase.Position) / DivisionDeplacement;

                    UpdatePath = false;
                }

                TempsÉcouléUpdatePath += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if ((TempsÉcouléDeplacement += (float)gameTime.ElapsedGameTime.TotalSeconds) > 0.03f)
                {
                    TempsÉcouléDeplacement = 0;

                    Position += Deplacement;

                    if (++compteur == DivisionDeplacement)
                    {
                        ActualCase = CloseList[1];
                        UpdatePath = true;
                        compteur = 0;
                    }
                }
            }
        }

        List<Case> OpenList;
        List<Case> CloseList;
        List<Case> Path;
        Case Target;

        void GererPathFinding()
        {
            OpenList = new List<Case>();
            CloseList = new List<Case>();
            Path = new List<Case>();

            Target = TrouverTarget();
            GenererHGCases();

            CloseList.Add(ActualCase);

            foreach (Case c in ActualCase.ListeParents)
                OpenList.Add(c);


            for(int i = 0; i < 20; ++i)
            {
                SearchOpen();

                Case currentCase = FindLowestCaseList(OpenList);

                OpenList.Remove(currentCase);
                CloseList.Add(currentCase);

                if (currentCase == Target)
                    break;
            }

        }

        Case TrouverTarget()
        {
            float distance = float.MaxValue;
            Case CaseTarget = new Case(Vector3.Zero, null);

            foreach(Case c in PointsDePatrouille.ListePoints)
            {
                float cDistance = Vector3.Distance(c.Position, MyPlayer.Position);

                if (cDistance < distance)
                {
                    distance = cDistance;
                    CaseTarget = c;
                }
            }

            return CaseTarget;
        }

        Case FindLowestCaseList(List<Case> ListeCases)
        {
            float petitF = ListeCases.Min(Case => Case.F);

            return ListeCases.Find(Case => Case.F == petitF);
        }

        void SearchOpen()
        {
            foreach(Case caseClose in CloseList)
            {
                foreach(Case c in caseClose.ListeParents)
                {
                    if (!OpenList.Contains(c))
                        OpenList.Add(c);
                }
            }
        }

        void GenererHGCases()
        {
            foreach(Case c in PointsDePatrouille.ListePoints)
            {
                c.H = Vector3.Distance(Target.Position, c.Position);
            }

            foreach(Case parent in PointsDePatrouille.ListePoints)
            {
                foreach(Case enfant in parent.ListeParents)
                {
                    enfant.G = Vector3.Distance(parent.Position, enfant.Position);
                }
            }
        }

        const float DISTANCE_MIN_SON = 130;
        void GererSound()
        {
            float distancePlayer = Vector3.Distance(Position, MyPlayer.Position);

            if(distancePlayer < DISTANCE_MIN_SON)
            {
                DroneSound.Play((DISTANCE_MIN_SON - distancePlayer) / DISTANCE_MIN_SON * 0.1f , 0, 0);
            }
        }

        void GererTir()
        {
            if(EstEnCollision())
            {
                BoundingSphere sphereCentreDrone  = SphèreDuDrone.Transform(Monde);
                sphereCentreDrone.Center += Vector3.UnitY * 3;
                Game.Components.Add(new RayonLaserCylindrique(Game, 1, Vector3.Zero, sphereCentreDrone.Center, Vector3.Normalize(CaméraJeu.Position - new Vector3(0, 15, 0) - Position),
                    new Vector2(0.1f, Vector3.Distance(CaméraJeu.Position, Position)), new Vector2(10, 10), "RedScreen", 0.01f, 0.1f));
                //Game.Components.Add(new RayonLaserCylindrique(Game, 1, Vector3.Zero, Position + (BoiteDeCollision.Max - BoiteDeCollision.Min) / 2, Vector3.Normalize(CaméraJeu.Position - new Vector3(0, 15, 0) - Position),
                //    new Vector2(0.1f, Vector3.Distance(CaméraJeu.Position, Position)), new Vector2(10, 10), "RedScreen", 0.01f, 0.1f));
                if (GenerateurRandom.Next(0,Data.ChanceDeTir) == 0)
                {
                    BulletHit.Play(0.3f, 0, 0);
                    MyPlayer.Health -= 5;
                }
                else
                {
                    BulletMiss.Play(0.1f, 0, 0);
                }
            }
        }

        float PlayerDistance;
        float WallDistance;
        public bool EstEnCollision()
        {
            Ray DroneRay;
            List<BoundingBox> ListeBoundingBox = new List<BoundingBox>();

            if (SphereDeTir.Intersects(MyPlayer.BoiteDeCollision))
            {
                foreach (ICollisionableList c in GameController.ListMurs)
                {
                    foreach (BoundingBox b in c.ListeBoundingBox)
                    {
                        if (SphereDeTir.Intersects(b))
                            ListeBoundingBox.Add(b);
                    }
                }

                PlayerDistance = Vector3.Distance(Position, CaméraJeu.Position);
                DroneRay = new Ray(Position, Vector3.Normalize(CaméraJeu.Position - Position));

                foreach (BoundingBox b in ListeBoundingBox)
                {
                    WallDistance = Vector3.Distance(GetRayBoundingBoxIntersectionPoint(DroneRay, b), CaméraJeu.Position);

                    if (WallDistance < PlayerDistance)
                        return false;
                }

                return true;

            }
            return false;
        }

        Vector3 GetRayBoundingBoxIntersectionPoint(Ray ray, BoundingBox box)
        {
            float? distance = ray.Intersects(box);
            if (distance != null)
                return ray.Position + ray.Direction * distance.Value;
            else
                return Vector3.One * float.MaxValue;
        }

        public bool EstEnCollision(ICollisionableList autreObjet)
        {
            return false;
        }
    }
}
