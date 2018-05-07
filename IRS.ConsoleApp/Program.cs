using Google.OrTools.ConstraintSolver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IRS.ConsoleApp
{
    class Program
    {
        /*
         *  值班安排： 
         *  先为每个值班段安排一名人员值班（第一轮），安排一轮后再进行第二轮、第三轮
         *  按优先级安排值班： 
         *  1.各个值班段可值班人员数少的优先安排值班
         *  2.在一个值班段选择人员时，优先选择总无课次数少的人
         *  注：人员在被安排后就会从该值班段中的无课人员集合中删除，这样之后就不用再遍历到；在一轮安排后开始下一轮时，每个值班段的排人顺序会重新调整（因为去掉了已被安排的人） 
         *  
         */ 
        static void Main()
        {
            User A = new User("A", 4);
            User B = new User("B", 3);
            User C = new User("C", 1);
            User D = new User("D", 2);
        
            DutyTime time1 = new DutyTime("1");
            DutyTime time2 = new DutyTime("2");
            DutyTime time3 = new DutyTime("3");
            DutyTime time4 = new DutyTime("4");
            DutyTime time5 = new DutyTime("5");

            time1.GetFreeUsers().Add(A);
            time2.GetFreeUsers().Add(A);
            time4.GetFreeUsers().Add(A);
            time5.GetFreeUsers().Add(A);
            time1.GetFreeUsers().Add(B);
            time2.GetFreeUsers().Add(B);
            time3.GetFreeUsers().Add(B);
            time3.GetFreeUsers().Add(C);
            time4.GetFreeUsers().Add(D);
            time5.GetFreeUsers().Add(D);

            List<DutyTime> dutyTimeList = new List<DutyTime>();
            dutyTimeList.Add(time1);
            dutyTimeList.Add(time2);
            dutyTimeList.Add(time3);
            dutyTimeList.Add(time4);
            dutyTimeList.Add(time5);

            for(int w = 0; w < 3; w++)
            {
                // 排序，可用于值班的人数少的值班段排在前面，下面安排值班是可用于值班的人数少的值班段优先安排  
                dutyTimeList = dutyTimeSort(dutyTimeList);
                for(int i =0; i<dutyTimeList.Count;i++)
                {
                    if(dutyTimeList[i].GetFreeUsers().Count > 0)
                    {
                        List<User> freeUserList = freeUserSort(dutyTimeList[i].GetFreeUsers());

                        dutyTimeList[i].GetDutyUsers().Add(freeUserList[0]);
                        Console.WriteLine(freeUserList[0].GetName() + " 被安排到" + dutyTimeList[i].GetTime() + "值班；该值班段现有" + dutyTimeList[i].GetDutyUsers().Count + "人值班，" + freeUserList[0].GetName() + "一周总无课次数为：" + freeUserList[0].GetNum());

                        User delUser = freeUserList[0];
                        for(int j = 0; j<dutyTimeList.Count(); j++)
                        {
                            if(dutyTimeList[j].GetFreeUsers().Contains(delUser))
                            {
                                dutyTimeList[j].GetFreeUsers().Remove(delUser);
                                Console.WriteLine("---" + delUser.GetName() + "已从" + dutyTimeList[j).getTime() + "值班段删除，目前该值班段剩余未被安排人数有：" + dutyTimeList.get(j).getFreeUsers().size());
                            }
                        }
                    }
                }
            }
        }

        public static List<User> freeUserSort(List<User> freeUserList)
        {
            freeUserList.Sort(delegate (User x, User y)
            {
                return x.GetNum().CompareTo(y.GetNum());
            });
            return freeUserList;
        }

        public static List<DutyTime> dutyTimeSort(List<DutyTime> dutyTimeList)
        {
            dutyTimeList.Sort(delegate (DutyTime x, DutyTime y)
            {
                return x.GetFreeUsers().Count.CompareTo(y.GetFreeUsers().Count);
            });
            return dutyTimeList;
        }
    }
}
