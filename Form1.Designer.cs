namespace Multiscale_Modeling
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.setNucletions_button = new System.Windows.Forms.Button();
            this.Run = new System.Windows.Forms.Button();
            this.save = new System.Windows.Forms.Button();
            this.load = new System.Windows.Forms.Button();
            this.numOfNucleations = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Clear = new System.Windows.Forms.Button();
            this.numOfInclusions = new System.Windows.Forms.TextBox();
            this.sizeOfInclusions = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.generateInclusions = new System.Windows.Forms.Button();
            this.inclusionType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.NeighborhoodType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Propability = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Substructure = new System.Windows.Forms.Button();
            this.DualPhase = new System.Windows.Forms.Button();
            this.SingleGrain = new System.Windows.Forms.Button();
            this.AllGrains = new System.Windows.Forms.Button();
            this.N = new System.Windows.Forms.TextBox();
            this.Niter = new System.Windows.Forms.TextBox();
            this.GenRand = new System.Windows.Forms.Button();
            this.RunMC = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(600, 600);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // setNucletions_button
            // 
            this.setNucletions_button.Location = new System.Drawing.Point(650, 54);
            this.setNucletions_button.Name = "setNucletions_button";
            this.setNucletions_button.Size = new System.Drawing.Size(105, 23);
            this.setNucletions_button.TabIndex = 1;
            this.setNucletions_button.Text = "Set nucletions";
            this.setNucletions_button.UseVisualStyleBackColor = true;
            this.setNucletions_button.Click += new System.EventHandler(this.seed_button_Click);
            // 
            // Run
            // 
            this.Run.Location = new System.Drawing.Point(650, 262);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(105, 23);
            this.Run.TabIndex = 2;
            this.Run.Text = "Run";
            this.Run.UseVisualStyleBackColor = true;
            this.Run.Click += new System.EventHandler(this.button1_Click);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(650, 349);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 23);
            this.save.TabIndex = 3;
            this.save.Text = "Save to file";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // load
            // 
            this.load.Location = new System.Drawing.Point(650, 389);
            this.load.Name = "load";
            this.load.Size = new System.Drawing.Size(75, 23);
            this.load.TabIndex = 4;
            this.load.Text = "Load from file";
            this.load.UseVisualStyleBackColor = true;
            this.load.Click += new System.EventHandler(this.load_Click);
            // 
            // numOfNucleations
            // 
            this.numOfNucleations.Location = new System.Drawing.Point(650, 28);
            this.numOfNucleations.Name = "numOfNucleations";
            this.numOfNucleations.Size = new System.Drawing.Size(105, 20);
            this.numOfNucleations.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(647, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Number of nucleation";
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(650, 291);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(105, 23);
            this.Clear.TabIndex = 7;
            this.Clear.Text = "Clear";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // numOfInclusions
            // 
            this.numOfInclusions.Location = new System.Drawing.Point(804, 28);
            this.numOfInclusions.Name = "numOfInclusions";
            this.numOfInclusions.Size = new System.Drawing.Size(111, 20);
            this.numOfInclusions.TabIndex = 8;
            // 
            // sizeOfInclusions
            // 
            this.sizeOfInclusions.Location = new System.Drawing.Point(804, 67);
            this.sizeOfInclusions.Name = "sizeOfInclusions";
            this.sizeOfInclusions.Size = new System.Drawing.Size(111, 20);
            this.sizeOfInclusions.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(801, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Number of inclusions";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(801, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Size of inclusions";
            // 
            // generateInclusions
            // 
            this.generateInclusions.Location = new System.Drawing.Point(804, 137);
            this.generateInclusions.Name = "generateInclusions";
            this.generateInclusions.Size = new System.Drawing.Size(111, 23);
            this.generateInclusions.TabIndex = 12;
            this.generateInclusions.Text = "Generate inclusions";
            this.generateInclusions.UseVisualStyleBackColor = true;
            this.generateInclusions.Click += new System.EventHandler(this.generateInclusions_Click);
            // 
            // inclusionType
            // 
            this.inclusionType.DisplayMember = "Text";
            this.inclusionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inclusionType.FormattingEnabled = true;
            this.inclusionType.Location = new System.Drawing.Point(804, 110);
            this.inclusionType.Name = "inclusionType";
            this.inclusionType.Size = new System.Drawing.Size(111, 21);
            this.inclusionType.TabIndex = 13;
            this.inclusionType.ValueMember = "ID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(801, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Inclusion type";
            // 
            // NeighborhoodType
            // 
            this.NeighborhoodType.DisplayMember = "Text";
            this.NeighborhoodType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NeighborhoodType.FormattingEnabled = true;
            this.NeighborhoodType.Location = new System.Drawing.Point(650, 221);
            this.NeighborhoodType.Name = "NeighborhoodType";
            this.NeighborhoodType.Size = new System.Drawing.Size(105, 21);
            this.NeighborhoodType.TabIndex = 15;
            this.NeighborhoodType.ValueMember = "ID";
            this.NeighborhoodType.SelectedIndexChanged += new System.EventHandler(this.NeighborhoodType_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(647, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Neighborhood type";
            // 
            // Propability
            // 
            this.Propability.Location = new System.Drawing.Point(761, 221);
            this.Propability.Name = "Propability";
            this.Propability.Size = new System.Drawing.Size(105, 20);
            this.Propability.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(758, 205);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Set Propability";
            // 
            // Substructure
            // 
            this.Substructure.Location = new System.Drawing.Point(761, 262);
            this.Substructure.Name = "Substructure";
            this.Substructure.Size = new System.Drawing.Size(105, 23);
            this.Substructure.TabIndex = 19;
            this.Substructure.Text = "Substructure";
            this.Substructure.UseVisualStyleBackColor = true;
            this.Substructure.Click += new System.EventHandler(this.Substructure_Click);
            // 
            // DualPhase
            // 
            this.DualPhase.Location = new System.Drawing.Point(762, 290);
            this.DualPhase.Name = "DualPhase";
            this.DualPhase.Size = new System.Drawing.Size(104, 23);
            this.DualPhase.TabIndex = 20;
            this.DualPhase.Text = "Dual Phase";
            this.DualPhase.UseVisualStyleBackColor = true;
            this.DualPhase.Click += new System.EventHandler(this.DualPhase_Click);
            // 
            // SingleGrain
            // 
            this.SingleGrain.Location = new System.Drawing.Point(872, 262);
            this.SingleGrain.Name = "SingleGrain";
            this.SingleGrain.Size = new System.Drawing.Size(104, 23);
            this.SingleGrain.TabIndex = 21;
            this.SingleGrain.Text = "Single grain";
            this.SingleGrain.UseVisualStyleBackColor = true;
            this.SingleGrain.Click += new System.EventHandler(this.SingleGrain_Click);
            // 
            // AllGrains
            // 
            this.AllGrains.Location = new System.Drawing.Point(872, 290);
            this.AllGrains.Name = "AllGrains";
            this.AllGrains.Size = new System.Drawing.Size(103, 23);
            this.AllGrains.TabIndex = 22;
            this.AllGrains.Text = "All Grains";
            this.AllGrains.UseVisualStyleBackColor = true;
            this.AllGrains.Click += new System.EventHandler(this.AllGrains_Click);
            // 
            // N
            // 
            this.N.Location = new System.Drawing.Point(836, 423);
            this.N.Name = "N";
            this.N.Size = new System.Drawing.Size(100, 20);
            this.N.TabIndex = 23;
            // 
            // Niter
            // 
            this.Niter.Location = new System.Drawing.Point(836, 455);
            this.Niter.Name = "Niter";
            this.Niter.Size = new System.Drawing.Size(100, 20);
            this.Niter.TabIndex = 24;
            // 
            // GenRand
            // 
            this.GenRand.Location = new System.Drawing.Point(942, 423);
            this.GenRand.Name = "GenRand";
            this.GenRand.Size = new System.Drawing.Size(75, 23);
            this.GenRand.TabIndex = 25;
            this.GenRand.Text = "Random";
            this.GenRand.UseVisualStyleBackColor = true;
            this.GenRand.Click += new System.EventHandler(this.GenRand_Click);
            // 
            // RunMC
            // 
            this.RunMC.Location = new System.Drawing.Point(942, 452);
            this.RunMC.Name = "RunMC";
            this.RunMC.Size = new System.Drawing.Size(75, 23);
            this.RunMC.TabIndex = 26;
            this.RunMC.Text = "Run MC";
            this.RunMC.UseVisualStyleBackColor = true;
            this.RunMC.Click += new System.EventHandler(this.RunMC_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 622);
            this.Controls.Add(this.RunMC);
            this.Controls.Add(this.GenRand);
            this.Controls.Add(this.Niter);
            this.Controls.Add(this.N);
            this.Controls.Add(this.AllGrains);
            this.Controls.Add(this.SingleGrain);
            this.Controls.Add(this.DualPhase);
            this.Controls.Add(this.Substructure);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Propability);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.NeighborhoodType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.inclusionType);
            this.Controls.Add(this.generateInclusions);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sizeOfInclusions);
            this.Controls.Add(this.numOfInclusions);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numOfNucleations);
            this.Controls.Add(this.load);
            this.Controls.Add(this.save);
            this.Controls.Add(this.Run);
            this.Controls.Add(this.setNucletions_button);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button setNucletions_button;
        private System.Windows.Forms.Button Run;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button load;
        private System.Windows.Forms.TextBox numOfNucleations;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.TextBox numOfInclusions;
        private System.Windows.Forms.TextBox sizeOfInclusions;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button generateInclusions;
        private System.Windows.Forms.ComboBox inclusionType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox NeighborhoodType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Propability;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Substructure;
        private System.Windows.Forms.Button DualPhase;
        private System.Windows.Forms.Button SingleGrain;
        private System.Windows.Forms.Button AllGrains;
        private System.Windows.Forms.TextBox N;
        private System.Windows.Forms.TextBox Niter;
        private System.Windows.Forms.Button GenRand;
        private System.Windows.Forms.Button RunMC;
    }
}

