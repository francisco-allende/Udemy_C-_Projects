using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MusicPlayer : Form
    { 
        //Listas globales con rutas y nombres de archivos
        List<string> ll_songNames;
        List<string> ll_songPaths;

        public MusicPlayer()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;

            //Voy abstrayendo el codigo en metodos
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                AddSongsToList(dialog.SafeFileNames.ToList(), dialog.FileNames.ToList());
            }
        }

        //No acepta canciones duplicadas, agregar dos veces la misma cancion. Solo las añado si retorna falso.
        private void AddSongsToList(List<string> names, List<string> paths)
        {
            //No puedo usar una lista sin ser inicializada. Solo haré una vez, en caso de que sea nula.
            if (ll_songNames == null)
            {
                ll_songNames = new List<string>();
            }

            if (ll_songPaths == null)
            {
                ll_songPaths = new List<string>();
            }

            foreach (var item in names)
            {
                if (!ExistsOnList(item))
                {
                    ll_songNames.Add(item);
                    ll_songPaths.Add(GetPath(item, paths));
                }
            }

            RefreshList();
        }

        //Itero para saber si ya existete o no. Si es true corto el bucle.
        private bool ExistsOnList(string song)
        {
            foreach(var item in ll_songNames)
            {
                if(item == song)
                {
                    return true;
                }
            }
            return false;
        }

        //matcheo el nombre del file con su ruta, la ruta siempre contendra al nombre.
        //si matchean nombre seleccionado con path, entonces ejecutame esa cancion
        private string GetPath(string fileName, List <string> songPath)
        {
            string pathNotFound = string.Empty;

            foreach (var path in songPath)
            {
                if(path.Contains(fileName))
                {
                    return path; //lo encontre, dejo de iterar y lo retorno, todo salio bien
                }
            }

            return pathNotFound; //si no lo encuentra, lo mejor es devolverlo vacio o hago quilombo
        }

        //capturo con doble click lo seleccionado en el list box
        private void songsList_DoubleClick(object sender, EventArgs e)
        {
            string fileName = songsList.Text;
            string runSongPath = GetPath(fileName, ll_songPaths);

            if (runSongPath != string.Empty)
            {
                axWindowsMediaPlayer1.URL = runSongPath;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //Valido que no venga vacio o tirar error de null reference
            if (songsList.Text != string.Empty) //es lo mismo que hacer "". 
            {
                ll_songNames.Remove(songsList.Text);
                ll_songPaths.Remove(GetPath(songsList.Text, ll_songPaths));
                RefreshList();
            }
        }

        //Importantisimo. Actualizo la BBDD (dataSource). Usado cuando añado canciones nuevas y cuando borro
        //Para actualizar, imprimero la vacio igualandola a null. Luego, cargo la nueva linked list.
        private void RefreshList()
        {
            songsList.DataSource = null;
            songsList.DataSource = ll_songNames;
        }//No posee parametros ya que songList y ll_songNames son globales

    
        private void songsList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void MusicPlayer_Load(object sender, EventArgs e)
        {

        }
    }
}
