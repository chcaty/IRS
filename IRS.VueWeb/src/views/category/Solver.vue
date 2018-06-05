<template>
    <div id="address">
        <div id="datagrid">
            <div class="toolbar">
                <el-button plain icon="el-icon-plus" @click="add()">添加</el-button>
            </div>
            <!-- 宿舍楼列表 -->
            <el-table :data="tableData" border style="width: 100%" :row-style=rowStyle :header-cell-style=rowStyle>
                <el-table-column prop="categoryInfoId" label="序号" width="70">
                </el-table-column>
                <el-table-column prop="categoryInfoName" label="处理结果名称" sortable width="180">
                </el-table-column>
                <el-table-column prop="categoryInfoOrder" label="处理结果排序" sortable width="200">
                </el-table-column>
                <el-table-column label="是否启用" width="180">
                    <template slot-scope="scope">
                        <el-switch v-model="scope.row.categoryInfoEnable" disabled :active-value="1" :inactive-value="0" active-text="启用" inactive-text="禁用" active-color="#13ce66" inactive-color="#ff4949"></el-switch>
                    </template>
                </el-table-column>
                <el-table-column prop="startFlag" label="是否为默认新增状态" width="150">
                    <template slot-scope="scope">
                        <i :class="scope.row.startFlag == '1' ? 'el-icon-check':'el-icon-close'"></i>
                    </template>
                </el-table-column>
                <el-table-column prop="endFlag" label="是否为默认完成状态" width="150">
                    <template slot-scope="scope">
                        <i :class="scope.row.endFlag == '1' ? 'el-icon-check':'el-icon-close'"></i>
                    </template>
                </el-table-column>
                <el-table-column label="操作">
                    <template slot-scope="scope">
                        <el-tooltip content="编辑" placement="right-end">
                            <el-button plain icon="el-icon-edit" type="primary" size="small" @click="edit(scope.row)"></el-button>
                        </el-tooltip>
                        <el-tooltip content="删除" placement="right-end" v-if="!(scope.row.name == 'admin') && !(scope.row.name == 'user')">
                            <el-button plain icon="el-icon-delete" type="danger" size="small" @click="del(scope.row)"></el-button>
                        </el-tooltip>
                    </template>
                </el-table-column>
            </el-table>

            <!-- 修改处理结果信息 -->
            <el-dialog title="修改处理结果信息" :visible.sync="editDialogFormVisible" @close="cancel()">
                <el-form ref="form" :model="form" label-position="top" label-width="120px">
                    <el-row class="first-row">
                        <el-col :span="12" class="first-column">
                            <el-form-item label="处理结果名称">
                                <el-input v-model="form.categoryInfoName" placeholder=''></el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :span="12">
                            <el-form-item label="处理结果排序">
                                <el-input v-model="form.categoryInfoOrder" placeholder=''></el-input>
                            </el-form-item>
                            <el-form-item label="处理结果Id" v-if="false">
                                <el-input v-model="form.categoryInfoId" v-if="false" placeholder=''></el-input>
                            </el-form-item>
                        </el-col>
                    </el-row>

                    <el-row class="last-row" style="height: 150px">
                        <el-col :span="12" class="first-column" style="height: 100%">
                            <el-form-item label="是否启用">
                                <el-switch v-model="form.categoryInfoEnable" :active-value="1" :inactive-value="0" active-text="启用" inactive-text="禁用" active-color="#13ce66" inactive-color="#ff4949">
                                </el-switch>
                            </el-form-item>
                        </el-col>
                        <el-col :span="12" style="height: 100%">
                            <el-form-item label="标志">
                                <el-radio-group v-model="form.startFlag">
                                    <el-radio :label="1">开始标志</el-radio>
                                    <el-radio :label="0">非开始标志</el-radio>
                                </el-radio-group>
                                <el-radio-group v-model="form.endFlag">
                                    <el-radio :label="1">结束标志</el-radio>
                                    <el-radio :label="0">非结束标志</el-radio>
                                </el-radio-group>
                            </el-form-item>
                            <el-form-item label="宿舍楼Id" v-if="false">
                                <el-input v-model="form.categoryInfoId" v-if="false" placeholder=''></el-input>
                            </el-form-item>
                        </el-col>
                    </el-row>
                </el-form>
                <span slot="footer" class="dialog-footer">
                    <el-button @click="cancel()">取 消</el-button>
                    <el-button type="primary" @click="save()">确 定</el-button>
                </span>
            </el-dialog>

        </div>
    </div>
</template>

<script>
    import {
        getInfo,
        getInfoById,
        addInfo,
        updateInfo,
        deleteInfoById,
        Model
    } from "@/api/solver";

    export default {
        data() {
            return {
                tableData: [],
                editDialogFormVisible: false, // 编辑和添加对话框开关
                uploadId: "",   // 更新的Id号
                form: new Model(), //  编辑和添加数据对应的模型
                isNew: false,    // 添加状态
                isEdit: false,   // 编辑状态
                defaultProps: {
                    children: 'children',
                    label: 'label'
                }
            }
        },
        methods: {
            rowStyle({ row, rowIndex}) {
                return 'text-align:center'
            },
            fetchData () {  // 获取数据列表
                getInfo().then(response => {
                    let result = response.data;
                    this.tableData = result;
                }).catch(() => { });
            },
            add () {  // 显示添加页面
                this.form = new Model();
                this.isNew = true;
                this.editDialogFormVisible = true;
            },
            edit (row) {  // 显示编辑页面
                let id = row.categoryInfoId;
                this.uploadId = id;
                getInfoById(id).then(response => {
                    let result = response.data;
                    this.form = result;
                    this.isEdit = true;
                    this.editDialogFormVisible = true;
                });
            },
            newData(){  // 保存新建数据
                this.isNew =false;
                addInfo(this.form).then(response => {
                    //成功执行内容
                    let result = response.status_code;
                    if (result == 200) {
                        this.$message({
                            type: 'success',
                            message: '创建新的处理结果信息成功',
                        })
                        this.fetchData()
                    }
                }).catch(err => {
                    let result = err.response.data
                    this.$message({
                        type: 'error',
                        message: result.message,
                    })
                })
            },
            updateData() {  // 保存更新数据
                this.isEdit =false;
                updateInfo(this.uploadId, this.form).then(response => {
                    //成功执行内容
                    let result = response.status_code;
                    if (result == 200) {
                        let currentId = this.form.categoryInfoId;
                        let record = 0;
                        record = this.tableData.findIndex((val, index) => {
                            return val.categoryInfoId == currentId;
                        });
                        this.tableData.splice(record, 1, this.form);
                        this.$message({
                            type: 'success',
                            message: '处理结果信息更改成功',
                        })
                        this.fetchData()
                    }
                })
                .catch((err) => {
                    console.log(err.response.data)
                });
            },
            cancel() { // 对话框中取消保存
                this.isEdit =false;
                this.isNew =false;
                this.editDialogFormVisible = false;
            },
            save() { // 对话框中保存数据
                this.editDialogFormVisible = false;
                if (this.isEdit && !this.isNew) {
                    this.updateData();
                }
                if (!this.isEdit && this.isNew) {
                    this.newData();
                }
            },
            del (row) { // 删除指定的数据
                this.$confirm("此操作将永久删除该信息, 是否继续?", "提示", {
                    confirmButtonText: "确定",
                    cancelButtonText: "取消",
                    type: "warning"
                }).then(() => {
                    let id = row.categoryInfoId;
                    let record = null;
                    record = this.tableData.findIndex(val => val.categoryInfoId == id);
                    deleteInfoById(id).then(response => {
                        let result = response.status_code;
                        if (result == 200) {
                            this.$message({
                                type: "success",
                                message: "删除成功!"
                            });
                            this.tableData.splice(record, 1);
                        }
                    })
                    .catch(() => {
                    });
                })
            }
        },
        mounted () {
            this.fetchData()
        },
    };

</script>

<style lang="scss">
@import "./../../styles/app-main.scss";
#address .el-form--label-top .el-form-item__label {
    width: 100%;
    text-align: center;
}
#address .el-form-item__content {
    text-align: center;
}

#address .first-row .el-col {
    border: 1px solid $border-color;
    border-left: 0px;
}
#address .first-row .first-column {
    border-left: 1px solid $border-color;
}

#address .last-row .el-col {
    border: 1px solid $border-color;
    border-top: 0px;
    border-left: 0px;
}
#address .last-row .first-column {
    border-left: 1px solid $border-color;
}
</style>
