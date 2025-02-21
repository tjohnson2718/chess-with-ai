using System.ComponentModel;
using System.Windows.Forms;

namespace ChessGame.View
{
    partial class MainMenuView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button_StartBlackVsComputerGame = new Button();
            label1 = new Label();
            button_StartWhiteVsComputerGame = new Button();
            button_StartHumanGame = new Button();
            button_Play690 = new Button();
            SuspendLayout();
            // 
            // button_StartBlackVsComputerGame
            // 
            button_StartBlackVsComputerGame.Dock = DockStyle.Bottom;
            button_StartBlackVsComputerGame.Font = new System.Drawing.Font("Segoe UI", 15.75F);
            button_StartBlackVsComputerGame.Location = new System.Drawing.Point(0, 558);
            button_StartBlackVsComputerGame.Margin = new Padding(71, 0, 71, 0);
            button_StartBlackVsComputerGame.Name = "button_StartBlackVsComputerGame";
            button_StartBlackVsComputerGame.Size = new System.Drawing.Size(505, 83);
            button_StartBlackVsComputerGame.TabIndex = 3;
            button_StartBlackVsComputerGame.Text = "Play as Black vs Computer";
            button_StartBlackVsComputerGame.UseVisualStyleBackColor = true;
            button_StartBlackVsComputerGame.Click += Button_StartBlackVsComputerGame_Click;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Top;
            label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            label1.Location = new System.Drawing.Point(0, 0);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(505, 83);
            label1.TabIndex = 100;
            label1.Text = "Welcome to to Ash's Chess Program. Have fun!";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_StartWhiteVsComputerGame
            // 
            button_StartWhiteVsComputerGame.Dock = DockStyle.Bottom;
            button_StartWhiteVsComputerGame.Font = new System.Drawing.Font("Segoe UI", 15.75F);
            button_StartWhiteVsComputerGame.Location = new System.Drawing.Point(0, 475);
            button_StartWhiteVsComputerGame.Margin = new Padding(71, 0, 71, 0);
            button_StartWhiteVsComputerGame.Name = "button_StartWhiteVsComputerGame";
            button_StartWhiteVsComputerGame.Size = new System.Drawing.Size(505, 83);
            button_StartWhiteVsComputerGame.TabIndex = 2;
            button_StartWhiteVsComputerGame.Text = "Play as White vs Computer";
            button_StartWhiteVsComputerGame.UseVisualStyleBackColor = true;
            button_StartWhiteVsComputerGame.Click += Button_StartWhiteVsComputerGame_Click;
            // 
            // button_StartHumanGame
            // 
            button_StartHumanGame.Dock = DockStyle.Bottom;
            button_StartHumanGame.Font = new System.Drawing.Font("Segoe UI", 15.75F);
            button_StartHumanGame.Location = new System.Drawing.Point(0, 392);
            button_StartHumanGame.Margin = new Padding(71, 0, 71, 0);
            button_StartHumanGame.Name = "button_StartHumanGame";
            button_StartHumanGame.Size = new System.Drawing.Size(505, 83);
            button_StartHumanGame.TabIndex = 1;
            button_StartHumanGame.Text = "Play Human vs Human";
            button_StartHumanGame.UseVisualStyleBackColor = true;
            button_StartHumanGame.Click += Button_StartHumanGame_Click;
            // 
            // button_Play690
            // 
            button_Play690.Dock = DockStyle.Bottom;
            button_Play690.Font = new System.Drawing.Font("Segoe UI", 15.75F);
            button_Play690.Location = new System.Drawing.Point(0, 309);
            button_Play690.Margin = new Padding(71, 0, 71, 0);
            button_Play690.Name = "button_Play690";
            button_Play690.Size = new System.Drawing.Size(505, 83);
            button_Play690.TabIndex = 4;
            button_Play690.Text = "Play Chess 690 (PvP)";
            button_Play690.UseVisualStyleBackColor = true;
            button_Play690.Click += Button_Start690_Click;
            // 
            // MainMenuView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new System.Drawing.Size(505, 641);
            Controls.Add(button_Play690);
            Controls.Add(button_StartHumanGame);
            Controls.Add(button_StartWhiteVsComputerGame);
            Controls.Add(label1);
            Controls.Add(button_StartBlackVsComputerGame);
            Margin = new Padding(4, 5, 4, 5);
            Name = "MainMenuView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Welcome";
            ResumeLayout(false);

        }

        #endregion
        private Button button_StartBlackVsComputerGame;
        private Label label1;
        private Button button_StartWhiteVsComputerGame;
        private Button button_StartHumanGame;
        private Button button_Play690;
    }
}