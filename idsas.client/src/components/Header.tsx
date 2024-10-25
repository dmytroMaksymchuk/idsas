import './Header.css';

interface HeaderProps {
    uploadFileActive: boolean;
    setUploadFileActive: (active: boolean) => void;
}
const Header = ({uploadFileActive, setUploadFileActive}: HeaderProps) => {
    const toggleUploadFileActive = () => {
        setUploadFileActive(!uploadFileActive);
    }
  return (
        <header className="app-header">
            <div className={"name"}>
                <h1>IDSAS </h1>
            </div>
            <img className={"upload-file-button"} src={"./upload-file.svg"} onClick={toggleUploadFileActive}></img>
        </header>
    );
};

export default Header;