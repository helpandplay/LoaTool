namespace LoaTool.Define.Interfaces;
public interface IDialog //Window Interface
{
    double Height { get; set; }
    object DataContext { get; set; }
    void Show();
    void Close();
    bool? ShowDialog();
    bool Focus();
    void Positioning(IDialog? parent = null);
}
