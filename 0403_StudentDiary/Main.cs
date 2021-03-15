using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace _0403_StudentDiary
{
    public partial class Main : Form
    {
        private string _filePath = Path.Combine(Environment.CurrentDirectory, "students.txt");

        private FileHelper<List<Student>> _fileHelper = new FileHelper<List<Student>>(Program.FilePath);
       
        public Main()
        {
           
            InitializeComponent();
            RefreshDiary();
            SetColumnsHeader();
        }
        
        private void RefreshDiary()
        {

            var students = _fileHelper.DeserializeFromFile();        
            if (comboBox2.SelectedItem == null || comboBox2.SelectedItem.ToString() == "Wszyscy")
            {
                dgvDiary.DataSource = students;
            }
            else if (comboBox2.SelectedItem.ToString() == "1")
            {
              
               dgvDiary.DataSource = students.FindAll(x => x.GroupId == "1");
            }
            else if (comboBox2.SelectedItem.ToString() == "2")
            {

                dgvDiary.DataSource = students.FindAll(x => x.GroupId == "2");
            }
            else if (comboBox2.SelectedItem.ToString() == "3")
            {

                dgvDiary.DataSource = students.FindAll(x => x.GroupId == "3");
            }
            else if (comboBox2.SelectedItem.ToString() == "4")
            {

                dgvDiary.DataSource = students.FindAll(x => x.GroupId == "4");
            }
            else if (comboBox2.SelectedItem.ToString() == "5")
            {

                dgvDiary.DataSource = students.FindAll(x => x.GroupId == "5");
            }
        }
       private void SetColumnsHeader()
        {
            dgvDiary.Columns[0].HeaderText = "Numer";
            dgvDiary.Columns[1].HeaderText = "Imie";
            dgvDiary.Columns[2].HeaderText = "Nazwisko";
            dgvDiary.Columns[3].HeaderText = "Uwagi";
            dgvDiary.Columns[4].HeaderText = "Matematyka";
            dgvDiary.Columns[5].HeaderText = "Technologia";
            dgvDiary.Columns[6].HeaderText = "Fizyka";
            dgvDiary.Columns[7].HeaderText = "Język Polski";
            dgvDiary.Columns[8].HeaderText = "Język Obcy";
            dgvDiary.Columns[9].HeaderText = "Zajęcia dodatkowe";
            dgvDiary.Columns[10].HeaderText = "Numer grupy";
        }

       
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addEditStudent = new AddEditStudent();
            addEditStudent.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvDiary.SelectedRows.Count==0)
            {
                MessageBox.Show("Proszę zaznacz ucznia, którego dane chcesz edytowac");
                return;
            }
            var addEditStudent = new AddEditStudent(Convert.ToInt32(dgvDiary.SelectedRows[0].Cells[0].Value));
            addEditStudent.ShowDialog();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDiary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Proszę zaznacz ucznia, którego  chcesz usunac");
                return;
            }
            var selectedStudent = dgvDiary.SelectedRows[0];
            var confirmDelete=MessageBox.Show($"Czy na pewno chcesz usunac ucznia{(selectedStudent.Cells[1].Value.ToString()+""+selectedStudent.Cells[2].Value.ToString()).Trim()}","Usuwanie ucznia",MessageBoxButtons.OKCancel);

            if (confirmDelete==DialogResult.OK)
            {
               DeleteStudent(Convert.ToInt32(selectedStudent.Cells[0].Value));
                RefreshDiary();
            }
        }
        private void DeleteStudent(int id)
        {
            var students = _fileHelper.DeserializeFromFile();
            students.RemoveAll(x => x.Id == id) ;
            _fileHelper.SerializeToFile(students);
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            
            RefreshDiary();
            
            
        }


    }
}
