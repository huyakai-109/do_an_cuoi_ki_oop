using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Final.Program;

namespace Final
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            List<NhanSu> list = new List<NhanSu>();
            NhanSu ns;
            ns = new NhanVien("1", "Lan", "145696", 20);
            list.Add(ns);
            ns = new NhanVien("2", "Long", "543616", 23);
            list.Add(ns);
            ns = new NhanVien("3", "Mạnh", "749251", 31);
            list.Add(ns);
            ns = new NhanVien("4", "Dũng", "840793", 25);
            list.Add(ns);
            ns = new NhanVien("5", "Chi", "230991", 23);
            list.Add(ns);
            ns = new NhanVien("6", "Thanh", "170932", 31);
            list.Add(ns);

            ns = new TruongPhong("7", "TP Mai", "987654", 23);
            list.Add(ns);
            ns = new TruongPhong("8", "TP Luân", "456789", 21);
            list.Add(ns);
            ns = new TruongPhong("9", "TP Kiệt", "123456", 23);
            list.Add(ns);

            ns = new GiamDoc("10", "GD Huy", "100903", 30, .8f);
            list.Add(ns);
            ns = new GiamDoc("11", "GD Huyền", "199614", 28, .2f);
            list.Add(ns);

            CongTy cty = new CongTy(list);

            // 1.Xuất danh sách nhân sự và lương của nhân sự
            cty.phanBoNhanVien();
            cty.tinhLuong();           
            Console.WriteLine("\nDanh sách nhân sự và lương của từng nhân sư:");
            cty.xuatDSNS();
            Console.WriteLine();

            // 2.Liệt kê (những) nhân viên có lương cao nhất
            Console.WriteLine("Nhân viên có lương cao nhất");
            ArrayList listLuong = cty.lietKeDSNVLuongCaoNhat();
            foreach(NhanVien nv in listLuong)
            {
                nv.xuat();
            }
            Console.WriteLine();

            // 3.Tổng lương của công ty phải trả cho nhân sự
            float total = cty.TotalSalary(TotalSalaryCalculate);    
            Console.WriteLine("Tổng lương của công ty : " + total);
            Console.WriteLine();

            // 4.Tổng thu nhập của từng giám đốc
            Console.WriteLine("Thu nhập của từng giám đốc");
            cty.dummyDataCongTy();
            cty.CalculateIncomeForCEO(total);
            
        }
      
        interface ICalculateSalary
        {
            float CalculateSalary();
        }
        public delegate float CalculateTotalSalary(NhanSu ns);
        public static float TotalSalaryCalculate(NhanSu ns)
        {
            return ns.CalculateSalary();
        }
        public abstract class NhanSu : ICalculateSalary
        {
            //1.Fields
            protected string maSo;
            protected string hoTen;
            protected string soDienThoai;
            protected float soNgayLamViec;
            protected float luong;

            //2.Get, set
            public string getMaSo()
            {
                return maSo;
            }
            public void setMaSo(string ma)
            {
                this.maSo = ma;
            }
            public string getHoTen()
            {
                return hoTen;
            }
            public void setHoTen(string ten)
            {
                this.hoTen = ten;
            }
            public string getSoDienThoai()
            {
                return soDienThoai;
            }
            public void setSoDienThoai(string soDT)
            {
                this.soDienThoai = soDT;
            }
            public float getSoNgayLamViec()
            {
                return soNgayLamViec;
            }
            public void setSoNgayLamViec(float ngay)
            {
                this.soNgayLamViec = ngay;
            }
            public float getLuong()
            {
                return luong;
            }
            public void setLuong(float luong)
            {
                this.luong = luong;
            }
            //3.Construcstors         
            public NhanSu(string maSo, string hoTen, string soDienThoai, float soNgayLamViec)
            {
                this.maSo = maSo;
                this.hoTen = hoTen;
                this.soDienThoai = soDienThoai;
                this.soNgayLamViec = soNgayLamViec;
            }
            public virtual void xuat()
            {
                Console.Write("Mã số: " + this.maSo + "\tTên: " + this.hoTen + "\tSố điện thoại: " + this.soDienThoai + "\tSố ngày làm việc: " + this.soNgayLamViec + "\t");
            }
            public void xuatMaVaTen()
            {
                Console.WriteLine("\tMã: " + this.maSo + "\tTên: " + this.hoTen);
            }
            public abstract float CalculateSalary();
        }
        internal class TruongPhong : NhanSu
        {
            //1.Fields
            private int soNVDuoiQuyen;
            static private int LUONG_1NGAY_TP = 200;
            static private int PHU_CAP_MOI_NV_QLY = 100;
            //2.Get, set
            public int getSoNvDuongQuyen()
            {
                return soNVDuoiQuyen;
            }
            public void setSoLuongNvDuongQuyen(int soLuongNvDuongQuyen)
            {
                this.soNVDuoiQuyen = soLuongNvDuongQuyen;
            }
            //3.Construcstors

            public TruongPhong(string maSo, string hoTen, string soDienThoai, float soNgayLamViec) // không cần thêm số lượng nhân viên dưới quyền vì mỗi lần thêm nhân sự tự động tăng lên theo phương thức tangNhanVien()   
                : base(maSo, hoTen, soDienThoai, soNgayLamViec)
            {
                this.soNVDuoiQuyen = 0;
            }
            public override void xuat()
            {
                base.xuat();
                Console.WriteLine("Số nhân viên: " + this.soNVDuoiQuyen + "\tLương: " + this.luong);
            }
            public override float CalculateSalary()
            {
                this.luong = this.soNgayLamViec * LUONG_1NGAY_TP + PHU_CAP_MOI_NV_QLY * this.soNVDuoiQuyen;
                return luong;
            }
            public void tangNhanVien()
            {
                this.soNVDuoiQuyen++; // Mỗi lần đưa nhân viên cho trưởng phòng quản lí thì tăng
            }
            public void giamNhanVien()
            {
                this.soNVDuoiQuyen--;
            }
        }
        class NhanVien : NhanSu
        {
            //1.Fields
            private TruongPhong truongPhong;
            static private int LUONG_1NGAY_NV = 100;
            //2.Get, set
            public TruongPhong getTruongPhong()
            {
                return truongPhong;
            }
            public void setTruongPhong(TruongPhong TP)
            {
                this.truongPhong = TP;
            }
            //3.Construcstors        
            public NhanVien(string maSo, string hoTen, string soDienThoai, float soNgayLamViec)
                : base(maSo, hoTen, soDienThoai, soNgayLamViec)
            {
                this.truongPhong = null;
                // vì lớp này để phân bổ trưởng phòng nên ta không thêm 
            }
            public override void xuat()
            {
                if (this.truongPhong != null) // vì nhân viên chưa chắc đc chỉ định trưởng phòng
                {
                    base.xuat();
                    Console.WriteLine("Lương: " + this.luong
                                     + "\tMã TP: " + this.truongPhong.getMaSo()
                                     + "\tTên TP: " + this.truongPhong.getHoTen());
                }
                else
                {
                    base.xuat();
                    Console.WriteLine("Lương: " + this.luong + "\tChưa phân bổ");
                }
            }
            public override float CalculateSalary()
            {
                this.luong = LUONG_1NGAY_NV * this.soNgayLamViec;
                return this.luong;
            }
        }
        class GiamDoc : NhanSu
        {
            //1.Fields
            private float coPhan;
            static private int LUONG_1NGAY_GD = 300;
            //2.Get, set
            public float getCoPhan()
            {
                return coPhan;
            }
            public void setCoPhan(float CP)
            {
                this.coPhan = CP;
            }
            //3.Construcstors          
            public GiamDoc(string maSo, string hoTen, string soDienThoai, float soNgayLamViec, float coPhan)
                : base(maSo, hoTen, soDienThoai, soNgayLamViec)
            {
                this.coPhan = coPhan;
            }
            public override void xuat()
            {
                base.xuat();
                Console.WriteLine("Số phần trăm cổ phần: " + this.coPhan + "\tLương: " + this.luong);
            }
            public override float CalculateSalary()
            {
                this.luong = LUONG_1NGAY_GD * this.soNgayLamViec;
                return (this.luong);
            }
        }

        internal class CongTy
        {
            //1.Fields
            private string tenCongTy;
            private string maSoThue;
            private float doanhThuThang;
            private List<NhanSu> listNS = new List<NhanSu>();
            //2.Get, set
            public string getTenCongTy()
            {
                return tenCongTy;
            }
            public void setTenCongTy(string ten)
            {
                this.tenCongTy = ten;
            }
            public string getMaSoThue()
            {
                return maSoThue;
            }
            public void setMaSoThue(string ma)
            {
                this.maSoThue = ma;
            }
            public float getDoanhThuThang()
            {
                return doanhThuThang;
            }
            public void setDoanhThuThang(float doanhThu)
            {
                this.doanhThuThang = doanhThu;
            }

            //3.Construcstors          
            public CongTy(List<NhanSu> list)
            {
                this.listNS = list;
            }
            public TruongPhong timTruongPhongTheoMa(string ma)
            {
                TruongPhong tp = null; // chưa tìm thấy
                foreach (NhanSu ns in this.listNS)
                {
                    if (ns is TruongPhong)
                    {
                        if (ns.getMaSo() == ma)
                        {
                            tp = (TruongPhong)ns;
                        }
                    }
                }
                return tp;
            }

            private TruongPhong chonTruongPhong()
            {
                TruongPhong tp = null;
                do
                {
                    Console.Write("Vui lòng chọn mã TP đã liệt kê trên: ");
                    string chonMa = Console.ReadLine();
                    // Bước 6: Kiểm tra mã có nằm trong ds không
                    tp = timTruongPhongTheoMa(chonMa);
                }
                while (tp == null);
                return tp;
            }
            public List<TruongPhong> layDSTruongPhong() // nên tạo ds chưa trưởng phòng để không mất công loop nhiều lần
            {
                List<TruongPhong> list = new List<TruongPhong>();
                foreach (NhanSu ns in this.listNS)
                {
                    if (ns is TruongPhong)
                    {
                        list.Add((TruongPhong)ns);
                    }
                }
                return list;
            }
            private void showMaVaTenTP()
            {
                Console.WriteLine("********** Danh sách trưởng phòng **********");
                // Bước 2
                List<TruongPhong> list = layDSTruongPhong();
                foreach (TruongPhong tp in list)
                {
                    tp.xuatMaVaTen();
                }
                Console.WriteLine("============================================");
            }
            public void lietKeDSTruongPhong()
            {
                foreach (NhanSu ns in listNS)
                {
                    if (ns is TruongPhong)
                    {
                        ns.xuatMaVaTen();
                    }
                }
            }
            private void phanBoChoMotNhanSu(NhanSu ns)
            {
                // ******************  start Phân bổ cho 1 nhân sự ****************************** //
                showMaVaTenTP();
                bool flag = true;
                do
                {
                    // Bước 3
                    Console.Write("Chọn 1 để phân bổ hoặc chọn 2 để đi tiếp: ");
                    int chon = int.Parse(Console.ReadLine());

                    switch (chon)
                    {
                        case 1:
                        case 2:
                            if (chon == 1) // Bước 5
                            {
                                TruongPhong tp = chonTruongPhong();
                                // Bước 7 đã có trưởng phòng lưu ở biến tp
                                // Bước 8 
                                ((NhanVien)ns).setTruongPhong(tp);
                                //Bước 10
                                tp.tangNhanVien();
                            }
                            flag = false;//Bước 4
                            break;
                        default:
                            Console.WriteLine("Chỉ được chọn 1 và 2");
                            flag = true;
                            break;
                    }

                } while (flag);
                // ******************  end Phân bổ cho 1 nhân sự ****************************** //
            }
            public void phanBoNhanVien()
            {
                //Bước 1
                foreach (NhanSu ns in listNS)
                {
                    if (ns is NhanVien)
                    {
                        // Bước 2
                        // In ra thông báo tên và mã nhân viên thường đề biết phân bổ
                        Console.Write("Đang phân bổ cho nhân viên: ");
                        ns.xuatMaVaTen();
                        phanBoChoMotNhanSu(ns);

                    }
                }
            }
            public void xuatDSNS()
            {
                foreach (NhanSu ns in listNS)
                {
                    ns.xuat();
                }
            }
            public void tinhLuong()
            {
                foreach (NhanSu ns in listNS)
                {
                    ns.CalculateSalary();
                }
            }
            public ArrayList lietKeDSNVLuongCaoNhat()
            {
                ArrayList list = new ArrayList();

                // 1. Tìm nhân viên đầu tiên
                //(vì trong trường hợp trong danh sách Tp đứng đầu nên không thề gán lương nhân viên đầu tiên là có lương cao nhất)
                NhanVien nv = null;
                int index = 0;
                foreach (NhanSu ns in listNS)
                {
                    index++;
                    if (ns is NhanVien)
                    {
                        nv = (NhanVien)ns;
                        break;
                    }
                }
                //2.Nếu có, thì cho nhân viên vừa tìm trong bước 1 là nhân viên có lương cao nhất
                if (nv != null)
                {
                    //3.Duyệt ds từ vị trí vừa tìm ở bước 2 và tìm nv có lương thật sự cao nhất
                    // int indexMax = 0;
                    for (int i = index - 1; i < listNS.Count; i++)
                    {
                        NhanSu current = (NhanSu)listNS[i];
                        if (current is NhanVien)
                        {
                            if (current.getLuong() > nv.getLuong())
                            {
                                nv = (NhanVien)current;
                                // indexMax = i;
                                index++;
                            }
                        }
                    }
                    list.Add(nv);
                    // 4.Duyệt lại ds và so sánh của nv đang duyệt  với nv max thực sự  ở bước 3 --> add vào ds    
                    for (int i = index; i < listNS.Count; i++)
                    {
                        NhanSu current = (NhanSu)listNS[i];
                        if (current is NhanVien)
                        {
                            if (current.getLuong() == nv.getLuong())
                            {
                                list.Add((NhanVien)current);
                            }
                        }
                    }
                }
                return list;
            }
            public float TotalSalary(CalculateTotalSalary salary)
            {
                float total = 0;
                foreach (NhanSu ns in listNS)
                {
                    total += salary(ns);
                }
                return total;
            }
            public void dummyDataCongTy()
            {
                this.tenCongTy = "Cyber - Learn";
                this.maSoThue = "10092003";
                this.doanhThuThang = 459634f;
            }
            public void CalculateIncomeForCEO(float totalSalary)
            {               
                foreach (var nhanSu in listNS)
                {
                    if (nhanSu is GiamDoc)
                    {
                        var giamDoc = nhanSu as GiamDoc;                       
                          float income = giamDoc.getLuong() + giamDoc.getCoPhan() * (this.doanhThuThang - totalSalary);
                        Console.WriteLine($"Thu nhập của giám đốc {giamDoc.getHoTen()} là:  {income}");
                    }
                }
              
            }
        } 

    }
}
