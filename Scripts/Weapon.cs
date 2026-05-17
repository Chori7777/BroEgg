namespace ProyectoSDL2.Engine.Scripts
{
    public class Weapon //Weapon n o necesita heredar de GameObject porque es una clase que se ocupa
                        //de agregar balas a una lista en el transform del player 
    {
        private Transform ownerTransform;
        private float timer = 0;
        private float fireRate = 1f;

        private int bulletWidth = 16;
        private int bulletHeight = 16;

        public Weapon(Transform ownerTransform)
        {
            this.ownerTransform = ownerTransform; //transform del player
        }

        public void Update()
        {
            timer += Program.DeltaTime;

            if (timer >= fireRate)
            {
                Shoot();
                timer = 0;
            }
        }

        private void Shoot()
        {
            Transform target = GetNearestEnemy();

            if (target == null) return;

            Bullet bullet = new Bullet(ownerTransform.PosX, ownerTransform.PosY, bulletWidth, bulletHeight, target);
            GameManager.Instance.LevelController.AddBullet(bullet);
        }

        private Transform GetNearestEnemy() //devuelve el transform del enemigo mas cercano
        {
            Transform nearest = null;
            float nearestDistance = float.MaxValue; //atributos afuera del metodo para que se reconozcan

            if (GameManager.Instance.LevelController.GameObjectsList.Count == 0) return null; //nos aseguramos que haya algo

            for (int i = 0; i < GameManager.Instance.LevelController.GameObjectsList.Count; i++) //recorremos la lista de GameObjects
            {
                GameObject enemy = GameManager.Instance.LevelController.GameObjectsList[i]; //guardamos al GameObject actual en
                                                                                            //una veriable de tipo GameObject (aislamos una
                                                                                            //de la lista para usarla)

                if(enemy is Enemy) //nos aseguramos que ese objeto sea realmente una bala para calcular su distancia
                {
                    float deltaX = enemy.Transform.PosX - ownerTransform.PosX;
                    float deltaY = enemy.Transform.PosY - ownerTransform.PosY;
                    float distance = MathF.Sqrt(deltaX * deltaX + deltaY * deltaY);

                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearest = enemy.Transform;
                    }
                }
            }
            return nearest; //devuelve el valor de el Enemy mas cercano
        }
    }
}