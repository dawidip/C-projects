using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Tree;

namespace TreePreview {
	public partial class MainWindow : Form {
		public MainWindow() {
			InitializeComponent();
            openFile(null, null);// add function to openning
        }

        private void generateTreeView(Tree.Node<Tree.Record> node, TreeNode nodePath)
        {
            int ind = 0;
            foreach (Tree.Node<Tree.Record> child in node.Children)
            {
                //treeView.Nodes[node.Data.ToString].Nodes.Add(child.Data);
                nodePath.Nodes.Add(child.Data.ToString());
                generateTreeView(child, nodePath.Nodes[ind++]);
            }
        }

        private void openFile(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            currentFileName = openFileDialog.FileName;

            root = Tree.XmlTree.Load(currentFileName);

            //right side of the box
            foreach (Tree.Node<Tree.Record> someNode in Tree.Tree.Traverse(root))
                preorderListBox.Items.Add(someNode.Data.ToString());
            

            //left side of the box

            TreeNode parentNode = new TreeNode(root.Data.ToString());
            treeView.Nodes.Add(parentNode);
            int tempind = 0;
            foreach (Tree.Node<Tree.Record> child in root.Children)
            {
                parentNode.Nodes.Add(child.Data.ToString());
                generateTreeView(child, parentNode.Nodes[tempind++]);
            }            
            treeView.Invalidate();  //draw tree

        }

        
        

        private void dodajFiltr(object sender, EventArgs e)
        {
            var addFilterDialogCurrent = new AddFilterDialog();
            if (addFilterDialogCurrent.ShowDialog() != DialogResult.OK) return;
            filterListBox.Items.Add(addFilterDialogCurrent.getFilterChosen);
            filterListBox.Items.Add("DUP");
        }

        private void saveFileClicked(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            currentFileName = saveFileDialog.FileName;

            System.Xml.Linq.XDocument doc = new XDocument(SaveElement(root));
            doc.Save(currentFileName);
        }

        private static XElement SaveElement(Node<Record> node)
        {
            XElement resultToReturn = new XElement("node");
            IEnumerable<XAttribute> attList = node.Data.Keys;
            foreach (XAttribute att in attList)
                resultToReturn.SetAttributeValue(att.Name, att.Value);

            resultToReturn.Add(node.Children.Select(e => SaveElement(e)));
            return resultToReturn;
        }

        private string currentFileName = "";
        Tree.Node<Tree.Record> root;

        private void usunFiltr(object sender, EventArgs e)
        {

        }
    
	}
}
