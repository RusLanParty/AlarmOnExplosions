using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Native;

namespace RealExplosions
{
    public class Main : Script
    {
        float radius;
        float radius2;
        float radius3;
        int time;
        public Main()
        {
            var dis = World.GetAllVehicles().Where(x => x.IsAlarmSounding).ToList();
            foreach(Vehicle v in dis)
            {
                v.AlarmTimeLeft = 0;
            }
            loadSettings();
            Tick += onTick;
        }
        void loadSettings()
        {
            radius = Settings.GetValue<float>("SETTINGS", "alarmRadius", 70f);
            radius2 = Settings.GetValue<float>("SETTINGS", "windowsRadius", 16f);
        }
        void onTick(object sendeer, EventArgs e)
        {
            Ped player = Game.Player.Character;
           
                List<Vehicle> vehs2 = new List<Vehicle>(World.GetNearbyVehicles(player, radius3)).Where(x => Function.Call<bool>(Hash.IS_ANY_PED_SHOOTING_IN_AREA, x.Position.X, x.Position.Y, x.Position.Z, 50f, 50f, 50f, true, true)).ToList();
                foreach (Vehicle v in vehs2)
                {
                    if (!v.IsEngineRunning && v.Driver == null)
                    {
                        Wait(57);
                        time = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 13000, 30000);
                        v.AlarmTimeLeft = time;
                        v.StartAlarm();
                    }
                }
            
            if(Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, -1, player.Position.X, player.Position.Y, player.Position.Z, radius * 2)) 
            {
                if(!Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, 3, player.Position.X, player.Position.Y, player.Position.Z, radius * 2) && !Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, 12, player.Position.X, player.Position.Y, player.Position.Z, radius * 2) && !Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, 11, player.Position.X, player.Position.Y, player.Position.Z, radius * 2) && !Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, 13, player.Position.X, player.Position.Y, player.Position.Z, radius * 2) && !Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, 14, player.Position.X, player.Position.Y, player.Position.Z, radius * 2) && !Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, 18, player.Position.X, player.Position.Y, player.Position.Z, radius * 2) && !Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, 19, player.Position.X, player.Position.Y, player.Position.Z, radius * 2) && !Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, 20, player.Position.X, player.Position.Y, player.Position.Z, radius * 2) && !Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, 21, player.Position.X, player.Position.Y, player.Position.Z, radius * 2) && !Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, 22, player.Position.X, player.Position.Y, player.Position.Z, radius * 2) && !Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, 30, player.Position.X, player.Position.Y, player.Position.Z, radius * 2) && !Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, 35, player.Position.X, player.Position.Y, player.Position.Z, radius * 2))
                {
                    List<Vehicle> vehs = new List<Vehicle>(World.GetNearbyVehicles(player, radius * 2)).Where(x => Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, -1, x.Position.X, x.Position.Y, x.Position.Z, radius)).ToList(); 
                    List<Vehicle> vehs1 = new List<Vehicle>(World.GetNearbyVehicles(player, radius * 2)).Where(x => Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, -1, x.Position.X, x.Position.Y, x.Position.Z, radius2)).ToList();
                    foreach(Vehicle v in vehs1)
                    {
                        for (int i = 0; i <= 7; i++)
                        {
                            Function.Call(Hash.SMASH_VEHICLE_WINDOW, v, i);
                        }
                    }
                         
                    foreach(Vehicle v in vehs)
                    {
                        if(!v.IsEngineRunning && v.Driver == null)
                        {
                            Wait(57);
                            time = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 13000, 22000);
                            v.AlarmTimeLeft = time;
                            v.StartAlarm();
                        }
                    }
                   
                }  
            }
        }
    }
}
