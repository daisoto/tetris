public class TetrominoFactory : IFactory<Tetromino>
{
    private TetrominoData tetrominoData = null;
    private Block block = null;

    public Tetromino Create()
    {
        if (tetrominoData == null)
        {
            return null;
        }
        else
        {
            //return new Tetromino(blockData.color, blockData.sprite);
        }

    }
}
