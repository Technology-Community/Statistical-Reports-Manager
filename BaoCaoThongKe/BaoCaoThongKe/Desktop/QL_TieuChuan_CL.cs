using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BaoCaoThongKe.Controller;
using BaoCaoThongKe.BusinessObject;
using BaoCaoThongKe.DataLayer;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevComponents.DotNetBar;
namespace BaoCaoThongKe.Desktop
{
    public partial class QL_TieuChuan_CL : Office2007Form
    {
        
        TieuChuanChatLuong_Ctrl CTRL = new TieuChuanChatLuong_Ctrl();

        public QL_TieuChuan_CL()
        {
            InitializeComponent();
        }

        private void QL_TieuChuan_CL_Load(object sender, EventArgs e)
        {
            CTRL.Show_DS_TieuChuan(bindingNavigator1,gridControl1);
            //
            gridView1.OptionsBehavior.Editable = false;
            //
            GridColumn gc = gridView1.Columns["MaTieuChuanCL"];
            gc.OptionsColumn.AllowEdit = false;
            gc.AppearanceCell.BackColor = Color.LemonChiffon;
        }

        private void BT_Them_Click(object sender, EventArgs e)
        {
            gridView1.OptionsBehavior.Editable = true;
            TangMaTuDong();
            BT_Them.Enabled = false;
        }
        public void TangMaTuDong()
        {
            //tang ma
            int ma;

            int phantu = 0;
            int dong = gridView1.RowCount;

            for (int i = 1; i <= dong; i++)
            {
                phantu = i;
            }

            if (phantu != 0)
            {
                //ma = (phantu + 1);
                ma = CTRL.GetLastID() + 1;
            }
            else
            {
                ma = 1;
            }
            //
            DataRowView row = (DataRowView)bindingNavigator1.BindingSource.AddNew();
            row["MaTieuChuanCL"] = ma;
        }

        private void BT_Xoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn Có Chắc Là Muốn Xóa Không?", "Quan ly Tieu chuan chat luong", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                bindingNavigator1.BindingSource.RemoveCurrent();
                CTRL.Update();
            }
        }

        private void BT_Sua_Click(object sender, EventArgs e)
        {
            gridView1.OptionsBehavior.Editable = true;
            gridView1.Focus();
            GridColumn gc = gridView1.Columns["TenTieuChuanCL"];
            gc.OptionsColumn.AllowEdit = true;
        }

        private void BT_save_Click(object sender, EventArgs e)
        {
            bindingNavigatorPositionItem.Focus();
            bindingNavigator1.BindingSource.MoveNext();
            CTRL.Update();
            BT_Them.Enabled = true;
        }

        private void BT_Xemlai_Click(object sender, EventArgs e)
        {
            CTRL.Show_DS_TieuChuan(bindingNavigator1, gridControl1);
            BT_Them.Enabled = true;
        }

        private void BT_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Delete Row?", "Quan ly Tieu chuan chat luong", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
                GridView view = sender as GridView;
                view.DeleteRow(view.FocusedRowHandle);
                CTRL.Update();
            }
            if (e.KeyCode == Keys.N && e.Modifiers == Keys.Control)
            {
                gridView1.OptionsBehavior.Editable = true;
                BT_save.Enabled = true;
                TangMaTuDong();
                BT_Them.Enabled = false;
            }
            if (e.KeyCode == Keys.S && e.Modifiers == Keys.Control)
            {
                bindingNavigatorPositionItem.Focus();
                CTRL.Update();
                BT_Them.Enabled = true;
            }
        }

        private void gridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            bindingNavigatorPositionItem.Focus();
            bindingNavigator1.BindingSource.MoveNext();
            CTRL.Update();
            gridView1.OptionsBehavior.Editable = true;
            BT_save.Enabled = true;
            TangMaTuDong();
        }

    }
}