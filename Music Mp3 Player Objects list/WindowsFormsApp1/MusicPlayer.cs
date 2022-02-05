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
        List<Song> ll_songs;

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
            if (ll_songs == null)
            {
                ll_songs = new List<Song>();
            }

            foreach (var item in names)
            {
                if (!ExistsOnList(item))
                {
                   ll_songs.Add(new Song(item, GetPath(item, paths)));
                }
            }

            RefreshList();
        }

        //Itero para saber si ya existete o no. Si es true corto el bucle.
        private bool ExistsOnList(string song)
        {
            foreach(var item in ll_songs)
            {
                if(item.Name == song)
                {
                    return true;
                }
            }
            return false;
        }

        //matcheo el nombre del file con su ruta, la ruta siempre contendra al nombre.
        //Recibe dos parametros. Si es addONList, viene lleno el songsPath
        //sino, viene nulo y por ende itero la lista de songs buscando el item.Path
        //no puedo iterar ll_songs en la primer iteracion porque es del addOnList y buscaria iterar una lista vacia
        //esto rompe el reproductor porque la lista esta vacia, aunque se imprima un nombre en pantalla
        private string GetPath(string fileName, List<string> songsPath = null)
        {
            string pathNotFound = string.Empty;

            if (songsPath == null)
            {
                foreach (var song in ll_songs)
                {
                    if (song.Name == fileName)
                    {
                        return song.Path; //lo encontre, dejo de iterar y retorno el path de esa cancion, todo salio bien
                    }
                }
            }
            else //else se activa cuando viene del add songs to list
            {
                foreach (var path in songsPath)
                {
                    if (path.Contains(fileName))
                    {
                        return path;
                    }
                }
            }

            return pathNotFound; //si no lo encuentra, lo mejor es devolverlo vacio o hago quilombo
        }

        //capturo con doble click lo seleccionado en el list box
        private void songsList_DoubleClick(object sender, EventArgs e)
        {
            string fileName = songsList.Text;
            string runSongPath = GetPath(fileName);

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
                Song songToRemove = null; //como borro objetos song, necesito hallarlo y vaciar sus campos

                foreach (var item in ll_songs)
                {
                    if (songsList.Text == item.Name)
                    {
                        item.Name = null;
                        item.Path = null;
                        songToRemove = item;
                    }
                }

                ll_songs.Remove(songToRemove); //ya puedo remover el vagon de la linked list, teniendo su memoria vaciada en ambas properites
                RefreshList();
            }
        }

        //Importantisimo. Actualizo la BBDD (dataSource). Usado cuando añado canciones nuevas y cuando borro
        //Para actualizar, imprimero la vacio igualandola a null. Luego, cargo la nueva linked list.
        private void RefreshList()
        {
            songsList.DataSource = null;

            var ll_names = new List<string>(); //creo una linked_list de names para enviar al datasource y la cargo

            foreach (var item in ll_songs)
            {
                ll_names.Add(item.Name);
            }

            songsList.DataSource = ll_names;
        }

    
        private void songsList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void MusicPlayer_Load(object sender, EventArgs e)
        {

        }
    }
}
