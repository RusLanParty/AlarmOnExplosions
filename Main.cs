using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        bool windows;
        bool gunshots;
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
            radius = Settings.GetValue<float>("SETTINGS", "expRadius", 50f);
            radius2 = Settings.GetValue<float>("SETTINGS", "windowsRadius", 15f);
            radius3 = Settings.GetValue<float>("SETTINGS", "gunShotRadius", 7f);
            windows = Settings.GetValue<bool>("SETTINGS", "windowsShatter", true);
            gunshots = Settings.GetValue<bool>("SETTINGS", "alarmOnGun", true);

        }
        void onTick(object sendeer, EventArgs e)
        {
            Ped player = Game.Player.Character;
           // GTA.UI.Screen.ShowSubtitle("CURWEP: " + player.Weapons.Current.LocalizedName);
            List<Ped> shts = new List<Ped>(World.GetNearbyPeds(player.Position, 100f)).Where(x => Function.Call<bool>(Hash.IS_PED_SHOOTING, x)).ToList();
           // GTA.UI.Screen.ShowHelpText("SHOTS FIRED shts size= " + shts.Count);
                foreach (Ped p in shts)
                {
                String wpg = p.Weapons.Current.Group.ToString();
                if (wpg != "Thrown")
                {
                    if((wpg == "Heavy" && p.Weapons.Current.LocalizedName.ToString() == "RPG") || (wpg == "Heavy" && p.Weapons.Current.LocalizedName.ToString() == "Homing Launcher") || (wpg == "Heavy" && p.Weapons.Current.LocalizedName.ToString() == "Railgun") || (wpg == "Heavy" && p.Weapons.Current.LocalizedName.ToString() == "Minigun") || (wpg != "Heavy")){
                        if (!Function.Call<bool>(Hash.IS_PED_CURRENT_WEAPON_SILENCED, p))
                        {
                            //GTA.UI.Screen.ShowSubtitle("HASH: " + wpg);
                            List<Vehicle> vehs2 = new List<Vehicle>(World.GetNearbyVehicles(p.Position, radius3));
                            // GTA.UI.Screen.ShowSubtitle("vehs2 size= " + vehs2.Count, 2000);
                            int counter = 0;
                            foreach (Vehicle v in vehs2)
                            {
                                int rnd = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 200);
                                if (!v.IsEngineRunning && v.Driver == null && rnd % 3 == 0)
                                {
                                    Wait(57);
                                    time = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 20000);
                                    v.AlarmTimeLeft = time;
                                    v.StartAlarm();
                                    counter++;
                                }
                                //GTA.UI.Screen.ShowHelpText("COUNT= " + counter, 1000, true, false);
                            }
                        }
                    }
                }
                }
            
            
           
            if (Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, -1, player.Position.X, player.Position.Y, player.Position.Z, radius * 2)) 
            {
                if(!Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, 3, player.Position.X, player.Position.Y, player.Position.Z, radius * 2) && !Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, 12, player.Position.X, player.Position.Y, player.Position.Z, radius * 2) && !Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, 11, player.Position.X, player.Position.Y, player.Position.Z, radius * 2) && !Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, 13, player.Position.X, player.Position.Y, player.Position.Z, radius * 2) && !Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, 14, player.Position.X, player.Position.Y, player.Position.Z, radius * 2) && !Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, 18, player.Position.X, player.Position.Y, player.Position.Z, radius * 2) && !Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, 19, player.Position.X, player.Position.Y, player.Position.Z, radius * 2) && !Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, 20, player.Position.X, player.Position.Y, player.Position.Z, radius * 2) && !Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, 21, player.Position.X, player.Position.Y, player.Position.Z, radius * 2) && !Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, 22, player.Position.X, player.Position.Y, player.Position.Z, radius * 2) && !Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, 30, player.Position.X, player.Position.Y, player.Position.Z, radius * 2) && !Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, 35, player.Position.X, player.Position.Y, player.Position.Z, radius * 2) && !Function.Call<bool>(Hash.IS_EXPLOSION_IN_SPHERE, 39, player.Position.X, player.Position.Y, player.Position.Z, radius * 2))
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
                    int counter = 0;
                    foreach(Vehicle v in vehs)
                    {
                        int rnd = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 200);
                        if (!v.IsEngineRunning && v.Driver == null && rnd % 3 == 0)
                        {
                            Wait(57);
                            time = Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, 0, 20000);
                            v.AlarmTimeLeft = time;
                            v.StartAlarm();
                            counter++;
                        }
                    }
                    //GTA.UI.Screen.ShowHelpText("COUNT= " + counter, 1000, true, false);
                }  
            }
        }
    }
}
