import './UploadFilePopUp.css';

interface UploadFilePopUpProps {
    handleFileChange: (event) => void;
    onSign: () => void;
    setUploadFileActive: (active: boolean) => void;
}
const UploadFilePopUp = ({handleFileChange, onSign, setUploadFileActive}: UploadFilePopUpProps) => {
    return (
        <div className={"file-uploading-container"}>
                <h2>Upload a File</h2>
                <input type="file" onChange={handleFileChange}/>
                <div className={"button-group"}>
                    <button className={"sign-button"} onClick={onSign}>Sign</button>
                </div>
                <img className={"close-button"} src={"./close.svg"} onClick={() => setUploadFileActive(false)} alt={""}></img>
        </div>
    );
};

export default UploadFilePopUp;