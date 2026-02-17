namespace HospitalCheckupSystem.Domain.Entities;

public class PhysicalExam
{
    public string GeneralAppeareance { get; private set; }
    public string Eyes { get; private set; }
    public string ENT {  get; private set; }
    public string Heart { get; private set; }
    public string Lungs { get; private set; }
    public string Abdomen { get; private set; }
    public string Neurology { get; private set; }

    private PhysicalExam() { }

    public PhysicalExam(
        string generalAppeareance, 
        string eyes, 
        string eNT, 
        string heart, 
        string lungs, 
        string abdomen, 
        string neurology)
    {
        GeneralAppeareance = generalAppeareance;
        Eyes = eyes;
        ENT = eNT;
        Heart = heart;
        Lungs = lungs;
        Abdomen = abdomen;
        Neurology = neurology;
    }
}
