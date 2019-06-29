using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using NPuzzle;
using NPuzzle.Models;
using System.Threading.Tasks;
using System.Threading;

namespace n_puzzle
{
    public class Program : Form
    {
        private Panel left_panel;
        private Panel title_panel;
        private Label label2;
        private Label puzzletype_label;
        private ComboBox puzzletype_comboBox;
        private Label heuristics_label;
        private ComboBox heuristics_comboBox;
        private Panel main_panel;
        private Label label5;
        private Button stop_button;
        private Button start_button;
        private Panel puzzle_panel;
        private Label initialState_label;
        private Label goalState_label;
        private Panel goal_panel;
        private Button button1;
        private Button aboutSoftware_button;
        private Panel aboutSoftware_panel;
        private Label description_label;
        private Label author_label;
        private Label error_message;
        private Panel loading_panel;
        private Label Processing_label;
        private ProgressBar progressBar1;
        private Label label1;
        private Button results_button;
        private Panel results_panel;
        private TextBox results_textBox;
        private Label results_label;
        private Label closed_label;
        private TextBox closed_textBox;
        private TextBox open_textbox;
        private Label label3;
        CancellationTokenSource cts = new CancellationTokenSource();

        public Program()
        {
            InitializeComponent();
        }

        static void Main(string[] args)
        {
            Application.Run(new Program());
        }

        private static void Display(Dictionary<int, List<Branch>> tree, TextBox tb)
        {
            tb.Text = "";

            foreach (var level in tree)
            {
                List<string> lines = new List<string>();

                foreach (var brunch in level.Value)
                {
                    for (int i = 0; i < brunch.Puzzle.template.GetLength(0); i++)
                    {
                        string line = string.Empty;
                        for (int j = 0; j < brunch.Puzzle.template.GetLength(1); j++)
                        {
                            if (j == 0)
                            {
                                line += "\t";
                            }

                            line += " " + (brunch.Puzzle.template[i, j] < 10 ? "0" + brunch.Puzzle.template[i, j] : brunch.Puzzle.template[i, j].ToString());
                        }

                        if (lines.Count < i + 1)
                        {
                            lines.Add(line);
                        }
                        else
                        {
                            lines[i] += line;
                        }

                    }
                    if (lines.Count == brunch.Puzzle.template.GetLength(0) + 1)
                    {
                        lines[brunch.Puzzle.template.GetLength(0)] += "\t g=" + brunch.G + ",h=" + brunch.H + ",f=" + brunch.F;
                    }
                    else
                    {
                        lines.Add("g=" + brunch.G + ",h=" + brunch.H + ",f=" + brunch.F);
                    }

                }

                lines.Add("");

                foreach (var line in lines)
                {
                    tb.AppendText(line.Trim());
                    tb.AppendText(Environment.NewLine);
                }
            }

            //Legenda
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine("zielony : znaleziona ścieżka");
            //Console.ForegroundColor = ConsoleColor.White;
            //Console.WriteLine("biały : inne ścieżki");
        }

        int NumberOfTilesMisplaced()
        {
            return 0;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Program));
            this.label1 = new System.Windows.Forms.Label();
            this.left_panel = new System.Windows.Forms.Panel();
            this.results_button = new System.Windows.Forms.Button();
            this.loading_panel = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.Processing_label = new System.Windows.Forms.Label();
            this.aboutSoftware_button = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.puzzletype_label = new System.Windows.Forms.Label();
            this.puzzletype_comboBox = new System.Windows.Forms.ComboBox();
            this.heuristics_label = new System.Windows.Forms.Label();
            this.heuristics_comboBox = new System.Windows.Forms.ComboBox();
            this.title_panel = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.main_panel = new System.Windows.Forms.Panel();
            this.error_message = new System.Windows.Forms.Label();
            this.goalState_label = new System.Windows.Forms.Label();
            this.goal_panel = new System.Windows.Forms.Panel();
            this.initialState_label = new System.Windows.Forms.Label();
            this.puzzle_panel = new System.Windows.Forms.Panel();
            this.stop_button = new System.Windows.Forms.Button();
            this.start_button = new System.Windows.Forms.Button();
            this.aboutSoftware_panel = new System.Windows.Forms.Panel();
            this.author_label = new System.Windows.Forms.Label();
            this.description_label = new System.Windows.Forms.Label();
            this.results_panel = new System.Windows.Forms.Panel();
            this.results_textBox = new System.Windows.Forms.TextBox();
            this.results_label = new System.Windows.Forms.Label();
            this.closed_label = new System.Windows.Forms.Label();
            this.closed_textBox = new System.Windows.Forms.TextBox();
            this.open_textbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.left_panel.SuspendLayout();
            this.loading_panel.SuspendLayout();
            this.title_panel.SuspendLayout();
            this.main_panel.SuspendLayout();
            this.aboutSoftware_panel.SuspendLayout();
            this.results_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "N puzzle";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // left_panel
            // 
            this.left_panel.Controls.Add(this.results_button);
            this.left_panel.Controls.Add(this.loading_panel);
            this.left_panel.Controls.Add(this.aboutSoftware_button);
            this.left_panel.Controls.Add(this.button1);
            this.left_panel.Controls.Add(this.puzzletype_label);
            this.left_panel.Controls.Add(this.puzzletype_comboBox);
            this.left_panel.Controls.Add(this.heuristics_label);
            this.left_panel.Controls.Add(this.heuristics_comboBox);
            this.left_panel.Controls.Add(this.title_panel);
            this.left_panel.Dock = System.Windows.Forms.DockStyle.Left;
            this.left_panel.Location = new System.Drawing.Point(0, 0);
            this.left_panel.Name = "left_panel";
            this.left_panel.Size = new System.Drawing.Size(201, 539);
            this.left_panel.TabIndex = 1;
            this.left_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.left_panel_Paint);
            // 
            // results_button
            // 
            this.results_button.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.results_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.results_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.results_button.Location = new System.Drawing.Point(10, 407);
            this.results_button.Name = "results_button";
            this.results_button.Size = new System.Drawing.Size(179, 35);
            this.results_button.TabIndex = 10;
            this.results_button.Text = "Wyniki";
            this.results_button.UseVisualStyleBackColor = false;
            this.results_button.Visible = false;
            this.results_button.Click += new System.EventHandler(this.results_button_Click);
            // 
            // loading_panel
            // 
            this.loading_panel.Controls.Add(this.progressBar1);
            this.loading_panel.Controls.Add(this.Processing_label);
            this.loading_panel.Location = new System.Drawing.Point(10, 448);
            this.loading_panel.Name = "loading_panel";
            this.loading_panel.Size = new System.Drawing.Size(179, 62);
            this.loading_panel.TabIndex = 9;
            this.loading_panel.Visible = false;
            this.loading_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.loading_panel_Paint);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(10, 39);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(156, 10);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 1;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // Processing_label
            // 
            this.Processing_label.AutoSize = true;
            this.Processing_label.Location = new System.Drawing.Point(6, 4);
            this.Processing_label.Name = "Processing_label";
            this.Processing_label.Size = new System.Drawing.Size(131, 21);
            this.Processing_label.TabIndex = 0;
            this.Processing_label.Text = "Przetwarzanie...";
            this.Processing_label.Click += new System.EventHandler(this.Processing_label_Click);
            // 
            // aboutSoftware_button
            // 
            this.aboutSoftware_button.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.aboutSoftware_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.aboutSoftware_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.aboutSoftware_button.Location = new System.Drawing.Point(10, 182);
            this.aboutSoftware_button.Name = "aboutSoftware_button";
            this.aboutSoftware_button.Size = new System.Drawing.Size(179, 35);
            this.aboutSoftware_button.TabIndex = 8;
            this.aboutSoftware_button.Text = "O programie";
            this.aboutSoftware_button.UseVisualStyleBackColor = false;
            this.aboutSoftware_button.Click += new System.EventHandler(this.aboutSoftware_button_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.button1.Location = new System.Drawing.Point(10, 128);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(179, 35);
            this.button1.TabIndex = 5;
            this.button1.Text = "Ustawienia";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // puzzletype_label
            // 
            this.puzzletype_label.AutoSize = true;
            this.puzzletype_label.Location = new System.Drawing.Point(12, 231);
            this.puzzletype_label.Name = "puzzletype_label";
            this.puzzletype_label.Size = new System.Drawing.Size(100, 21);
            this.puzzletype_label.TabIndex = 4;
            this.puzzletype_label.Text = "Rodzaj puzli";
            this.puzzletype_label.Click += new System.EventHandler(this.label4_Click);
            // 
            // puzzletype_comboBox
            // 
            this.puzzletype_comboBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.puzzletype_comboBox.FormattingEnabled = true;
            this.puzzletype_comboBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.puzzletype_comboBox.Items.AddRange(new object[] {
            "3x3",
            "4x4",
            "5x5",
            "8x8",
            "12x12"});
            this.puzzletype_comboBox.Location = new System.Drawing.Point(12, 255);
            this.puzzletype_comboBox.Name = "puzzletype_comboBox";
            this.puzzletype_comboBox.Size = new System.Drawing.Size(179, 29);
            this.puzzletype_comboBox.TabIndex = 3;
            this.puzzletype_comboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // heuristics_label
            // 
            this.heuristics_label.AutoSize = true;
            this.heuristics_label.Location = new System.Drawing.Point(12, 303);
            this.heuristics_label.Name = "heuristics_label";
            this.heuristics_label.Size = new System.Drawing.Size(94, 21);
            this.heuristics_label.TabIndex = 2;
            this.heuristics_label.Text = "Heurystyka";
            this.heuristics_label.Click += new System.EventHandler(this.label3_Click);
            // 
            // heuristics_comboBox
            // 
            this.heuristics_comboBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.heuristics_comboBox.FormattingEnabled = true;
            this.heuristics_comboBox.Items.AddRange(new object[] {
            "Manhattan Distance",
            "Hamming Distance"});
            this.heuristics_comboBox.Location = new System.Drawing.Point(12, 327);
            this.heuristics_comboBox.Name = "heuristics_comboBox";
            this.heuristics_comboBox.Size = new System.Drawing.Size(179, 29);
            this.heuristics_comboBox.TabIndex = 1;
            this.heuristics_comboBox.SelectedIndexChanged += new System.EventHandler(this.heuristics_comboBox_SelectedIndexChanged);
            // 
            // title_panel
            // 
            this.title_panel.BackColor = System.Drawing.Color.SlateGray;
            this.title_panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.title_panel.Controls.Add(this.label5);
            this.title_panel.Controls.Add(this.label2);
            this.title_panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.title_panel.Location = new System.Drawing.Point(0, 0);
            this.title_panel.Name = "title_panel";
            this.title_panel.Size = new System.Drawing.Size(201, 111);
            this.title_panel.TabIndex = 0;
            this.title_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(79, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 21);
            this.label5.TabIndex = 1;
            this.label5.Text = "A*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(34, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 33);
            this.label2.TabIndex = 0;
            this.label2.Text = "N Puzzle";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // main_panel
            // 
            this.main_panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.main_panel.Controls.Add(this.error_message);
            this.main_panel.Controls.Add(this.goalState_label);
            this.main_panel.Controls.Add(this.goal_panel);
            this.main_panel.Controls.Add(this.initialState_label);
            this.main_panel.Controls.Add(this.puzzle_panel);
            this.main_panel.Controls.Add(this.stop_button);
            this.main_panel.Controls.Add(this.start_button);
            this.main_panel.Location = new System.Drawing.Point(201, 0);
            this.main_panel.Name = "main_panel";
            this.main_panel.Size = new System.Drawing.Size(875, 539);
            this.main_panel.TabIndex = 2;
            this.main_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.main_panel_Paint);
            // 
            // error_message
            // 
            this.error_message.AutoSize = true;
            this.error_message.BackColor = System.Drawing.Color.Red;
            this.error_message.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.error_message.Location = new System.Drawing.Point(20, 463);
            this.error_message.Name = "error_message";
            this.error_message.Size = new System.Drawing.Size(44, 21);
            this.error_message.TabIndex = 7;
            this.error_message.Text = "Error";
            this.error_message.Visible = false;
            // 
            // goalState_label
            // 
            this.goalState_label.AutoSize = true;
            this.goalState_label.Location = new System.Drawing.Point(633, 21);
            this.goalState_label.Name = "goalState_label";
            this.goalState_label.Size = new System.Drawing.Size(36, 21);
            this.goalState_label.TabIndex = 6;
            this.goalState_label.Text = "Cel";
            this.goalState_label.Click += new System.EventHandler(this.label3_Click_2);
            // 
            // goal_panel
            // 
            this.goal_panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.goal_panel.Location = new System.Drawing.Point(446, 45);
            this.goal_panel.Name = "goal_panel";
            this.goal_panel.Size = new System.Drawing.Size(400, 400);
            this.goal_panel.TabIndex = 5;
            this.goal_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.goal_panel_Paint);
            // 
            // initialState_label
            // 
            this.initialState_label.AutoSize = true;
            this.initialState_label.Location = new System.Drawing.Point(141, 21);
            this.initialState_label.Name = "initialState_label";
            this.initialState_label.Size = new System.Drawing.Size(146, 21);
            this.initialState_label.TabIndex = 4;
            this.initialState_label.Text = "Stan początkowy";
            this.initialState_label.Click += new System.EventHandler(this.label3_Click_1);
            // 
            // puzzle_panel
            // 
            this.puzzle_panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.puzzle_panel.Location = new System.Drawing.Point(20, 45);
            this.puzzle_panel.Name = "puzzle_panel";
            this.puzzle_panel.Size = new System.Drawing.Size(400, 400);
            this.puzzle_panel.TabIndex = 3;
            this.puzzle_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.puzzle_panel_Paint);
            // 
            // stop_button
            // 
            this.stop_button.BackColor = System.Drawing.Color.Brown;
            this.stop_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.stop_button.Enabled = false;
            this.stop_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.stop_button.Location = new System.Drawing.Point(446, 473);
            this.stop_button.Name = "stop_button";
            this.stop_button.Size = new System.Drawing.Size(90, 35);
            this.stop_button.TabIndex = 2;
            this.stop_button.Text = "Stop";
            this.stop_button.UseVisualStyleBackColor = false;
            this.stop_button.Click += new System.EventHandler(this.stop_button_Click);
            // 
            // start_button
            // 
            this.start_button.BackColor = System.Drawing.Color.RoyalBlue;
            this.start_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.start_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.start_button.Location = new System.Drawing.Point(330, 473);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(90, 35);
            this.start_button.TabIndex = 1;
            this.start_button.Text = "Start";
            this.start_button.UseVisualStyleBackColor = false;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // aboutSoftware_panel
            // 
            this.aboutSoftware_panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.aboutSoftware_panel.Controls.Add(this.author_label);
            this.aboutSoftware_panel.Controls.Add(this.description_label);
            this.aboutSoftware_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aboutSoftware_panel.Location = new System.Drawing.Point(201, 0);
            this.aboutSoftware_panel.Name = "aboutSoftware_panel";
            this.aboutSoftware_panel.Size = new System.Drawing.Size(875, 539);
            this.aboutSoftware_panel.TabIndex = 7;
            // 
            // author_label
            // 
            this.author_label.AutoSize = true;
            this.author_label.Location = new System.Drawing.Point(57, 325);
            this.author_label.Name = "author_label";
            this.author_label.Size = new System.Drawing.Size(212, 21);
            this.author_label.TabIndex = 1;
            this.author_label.Text = "Autor: Marcin Łukaszewski";
            this.author_label.Click += new System.EventHandler(this.label4_Click_2);
            // 
            // description_label
            // 
            this.description_label.Location = new System.Drawing.Point(57, 137);
            this.description_label.Name = "description_label";
            this.description_label.Size = new System.Drawing.Size(784, 157);
            this.description_label.TabIndex = 0;
            this.description_label.Text = resources.GetString("description_label.Text");
            this.description_label.Click += new System.EventHandler(this.description_label_Click);
            // 
            // results_panel
            // 
            this.results_panel.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.results_panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.results_panel.Controls.Add(this.open_textbox);
            this.results_panel.Controls.Add(this.label3);
            this.results_panel.Controls.Add(this.closed_textBox);
            this.results_panel.Controls.Add(this.closed_label);
            this.results_panel.Controls.Add(this.results_label);
            this.results_panel.Controls.Add(this.results_textBox);
            this.results_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.results_panel.Location = new System.Drawing.Point(201, 0);
            this.results_panel.Name = "results_panel";
            this.results_panel.Size = new System.Drawing.Size(875, 539);
            this.results_panel.TabIndex = 8;
            this.results_panel.Visible = false;
            // 
            // results_textBox
            // 
            this.results_textBox.Location = new System.Drawing.Point(96, 147);
            this.results_textBox.Multiline = true;
            this.results_textBox.Name = "results_textBox";
            this.results_textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.results_textBox.Size = new System.Drawing.Size(682, 337);
            this.results_textBox.TabIndex = 0;
            // 
            // results_label
            // 
            this.results_label.AutoSize = true;
            this.results_label.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.results_label.Location = new System.Drawing.Point(373, 54);
            this.results_label.Name = "results_label";
            this.results_label.Size = new System.Drawing.Size(107, 30);
            this.results_label.TabIndex = 1;
            this.results_label.Text = "Rezultat";
            this.results_label.Click += new System.EventHandler(this.results_label_Click);
            // 
            // closed_label
            // 
            this.closed_label.AutoSize = true;
            this.closed_label.Location = new System.Drawing.Point(92, 106);
            this.closed_label.Name = "closed_label";
            this.closed_label.Size = new System.Drawing.Size(77, 21);
            this.closed_label.TabIndex = 2;
            this.closed_label.Text = "CLOSED:";
            // 
            // closed_textBox
            // 
            this.closed_textBox.Enabled = false;
            this.closed_textBox.Location = new System.Drawing.Point(187, 103);
            this.closed_textBox.Name = "closed_textBox";
            this.closed_textBox.Size = new System.Drawing.Size(100, 27);
            this.closed_textBox.TabIndex = 3;
            // 
            // open_textbox
            // 
            this.open_textbox.Enabled = false;
            this.open_textbox.Location = new System.Drawing.Point(678, 100);
            this.open_textbox.Name = "open_textbox";
            this.open_textbox.Size = new System.Drawing.Size(100, 27);
            this.open_textbox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(599, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "OPEN:";
            // 
            // Program
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(1076, 539);
            this.Controls.Add(this.results_panel);
            this.Controls.Add(this.aboutSoftware_panel);
            this.Controls.Add(this.left_panel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.main_panel);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Snow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Program";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Program_Load);
            this.left_panel.ResumeLayout(false);
            this.left_panel.PerformLayout();
            this.loading_panel.ResumeLayout(false);
            this.loading_panel.PerformLayout();
            this.title_panel.ResumeLayout(false);
            this.title_panel.PerformLayout();
            this.main_panel.ResumeLayout(false);
            this.main_panel.PerformLayout();
            this.aboutSoftware_panel.ResumeLayout(false);
            this.aboutSoftware_panel.PerformLayout();
            this.results_panel.ResumeLayout(false);
            this.results_panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Program_Load(object sender, EventArgs e)
        {
            this.aboutSoftware_panel.Hide();
            this.main_panel.Show();

            List<PuzzleType> puzzleTypes = new List<PuzzleType>();
            puzzleTypes.Add(new PuzzleType() { N = 3, DisplayName = "3x3" });
            puzzleTypes.Add(new PuzzleType() { N = 4, DisplayName = "4x4" });
            puzzleTypes.Add(new PuzzleType() { N = 5, DisplayName = "5x5" });
            puzzleTypes.Add(new PuzzleType() { N = 8, DisplayName = "8x8" });

            puzzletype_comboBox.DataSource = puzzleTypes;
            puzzletype_comboBox.ValueMember = "N";
            puzzletype_comboBox.DisplayMember = "DisplayName";

            List<HeuristicsTypes> types = new List<HeuristicsTypes>();
            types.Add(new HeuristicsTypes() { ID = 1, DisplayName = "Manhattan Distance" });
            types.Add(new HeuristicsTypes() { ID = 2, DisplayName = "Hamming Distance" });

            heuristics_comboBox.DataSource = types;
            heuristics_comboBox.ValueMember = "ID";
            heuristics_comboBox.DisplayMember = "DisplayName";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            puzzle_panel.Controls.Clear();
            goal_panel.Controls.Clear();

            PuzzleType type = puzzletype_comboBox.SelectedItem as PuzzleType;
            int size = 400 / type.N;
            int k = 1;
            for (int i = 0; i < type.N; i++)
            {
                for (int j = 0; j < type.N; j++)
                {
                    // initial state
                    NumericUpDown iT = new NumericUpDown();
                    iT.Name = "i_" + i.ToString() + "," + j.ToString();
                    iT.Width = size;
                    iT.Height = size;
                    iT.Top = i * size;
                    iT.Left = j * size;
                    iT.Controls.RemoveAt(0);
                    this.puzzle_panel.Controls.Add(iT);

                    // goal state
                    NumericUpDown gT = new NumericUpDown();
                    gT.Name = "g_" + i.ToString() + "," + j.ToString();
                    gT.Width = size;
                    gT.Height = size;
                    gT.Top = i * size;
                    gT.Left = j * size;
                    gT.Controls.RemoveAt(0);
                    gT.Text = (k).ToString();

                    if (i != type.N - 1 || j != type.N - 1)
                    {
                        gT.Text = (k).ToString();
                    }
                    else {
                        gT.Text = (0).ToString();
                    }

                    this.goal_panel.Controls.Add(gT);
                    k++;
                }
            }
           

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void heuristics_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void stop_button_Click(object sender, EventArgs e)
        {
            cts.Cancel();
        }

        private void left_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void puzzle_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click_2(object sender, EventArgs e)
        {

        }

        private async void start_button_Click(object sender, EventArgs e)
        {
            this.results_button.Visible = false;
            this.puzzletype_comboBox.Enabled = false;
            this.heuristics_comboBox.Enabled = false;
            this.start_button.Enabled = false;
            this.stop_button.Enabled = true;
            this.loading_panel.Visible = true;
            this.error_message.Visible = false;
            PuzzleType type = puzzletype_comboBox.SelectedItem as PuzzleType;

            int[,] initial = new int[type.N, type.N];
            // prepare initial state
            foreach (Control item in puzzle_panel.Controls.OfType<NumericUpDown>())
            {
                string location = item.Name.Replace("i_", "");
                int i = int.Parse(location.Split(',')[0]);
                int j = int.Parse(location.Split(',')[1]);

                if (string.IsNullOrEmpty(item.Text))
                {
                    initial[i,j] = 0;
                }
                else
                {
                    initial[i,j] = int.Parse(item.Text);
                }
            }

            Puzzle initialState = new Puzzle(initial);

            int[,] goal = new int[type.N, type.N];
            // prepare goal state
            foreach (Control item in goal_panel.Controls.OfType<NumericUpDown>())
            {

                string location = item.Name.Replace("g_", "");
                int i = int.Parse(location.Split(',')[0]);
                int j = int.Parse(location.Split(',')[1]);

                if (string.IsNullOrEmpty(item.Text))
                {
                    goal[i, j] = 0;
                }
                else
                {
                    goal[i, j] = int.Parse(item.Text);
                }
            }

            Puzzle goalState = new Puzzle(goal);

            try
            {
                AStar aStar = new AStar(initialState, goalState);
                Result result = await Task.Run(() => aStar.Run(cts.Token));

                this.results_button.Visible = true;
                this.results_panel.Visible = true;
                this.main_panel.Visible = false;
                this.aboutSoftware_panel.Visible = false;
                this.closed_textBox.Text = result.Closed.ToString();
                this.open_textbox.Text = result.Open.ToString();

                Dictionary<int, List<Branch>> tree = new Dictionary<int, List<Branch>>();
                foreach (var item in result.Path)
                {
                    if (tree.ContainsKey(item.G))
                    {
                        tree[item.G].Add(item);
                    }
                    else
                    {
                        tree.Add(item.G, new List<Branch> { item });
                    }
                }
                Display(tree, results_textBox);
            }
            catch (OperationCanceledException)
            {
                cts = new CancellationTokenSource();
            }
            catch (Exception ex)
            {
                this.error_message.Text = "Błąd: " + ex.Message;
                this.error_message.Visible = true;
            }
            finally {
                this.loading_panel.Visible = false;
                this.puzzletype_comboBox.Enabled = true;
                this.heuristics_comboBox.Enabled = true;
                this.start_button.Enabled = true;
                this.stop_button.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.aboutSoftware_panel.Hide();
            this.results_panel.Hide();
            this.main_panel.Show();
        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void aboutSoftware_button_Click(object sender, EventArgs e)
        {
            this.main_panel.Hide();
            this.results_panel.Hide();
            this.aboutSoftware_panel.Show();

        }

        private void description_label_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click_2(object sender, EventArgs e)
        {

        }

        private void goal_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void loading_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Processing_label_Click(object sender, EventArgs e)
        {

        }

        private void results_button_Click(object sender, EventArgs e)
        {
            this.results_panel.Visible = true;
            this.main_panel.Visible = false;
            this.aboutSoftware_panel.Visible = false;
        }

        private void main_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void results_label_Click(object sender, EventArgs e)
        {

        }
    }


}
