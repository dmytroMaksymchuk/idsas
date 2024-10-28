import './Header.css';

interface HeaderProps {
    uploadFileActive: boolean;
    setUploadFileActive: (active: boolean) => void;
    userIndex: number;
    setUserIndex: (index: number) => void;
}
const Header = ({uploadFileActive, setUploadFileActive, userIndex, setUserIndex}: HeaderProps) => {
    const toggleUploadFileActive = () => {
        setUploadFileActive(!uploadFileActive);
    }
  return (
        <header className="app-header">
            <div className={"name"}>
                <h1>IDSAS </h1>
            </div>
            <div className={"user-selected-buttons"}>
                <img className={"user-button " + (userIndex === 0 ? "selected" : "")} src={"./user_A.png"} onClick={() => setUserIndex(0)} alt={""}/>
                <img className={"user-button " + (userIndex === 1 ? "selected" : "")} src={"./user_B.png"} onClick={() => setUserIndex(1)} alt={""}/>
                <img className={"user-button " + (userIndex === 2 ? "selected" : "")} src={"./user_C.png"} onClick={() => setUserIndex(2)} alt={""}/>
            </div>
            <img className={"upload-file-button"} src={"./upload-file.svg"} onClick={toggleUploadFileActive} />
        </header>
    );
};

export default Header;